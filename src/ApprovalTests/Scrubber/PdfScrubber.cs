using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApprovalTests.Scrubber
{
    static class PdfScrubber
    {
        public static void ScrubPdf(string pdfFilePath)
        {
            using var fileStream = File.Open(pdfFilePath, FileMode.Open);
            var replacements = FindReplacements(fileStream);
            WriteReplacements(fileStream, replacements);
        }

        public class Replacement
        {
            public long start;
            public string text;

            public override string ToString()
            {
                return $"{nameof(start)}: {start}, {nameof(text)}: {text}";
            }
        }

        static void WriteReplacements(FileStream fileStream, IEnumerable<Replacement> replacements)
        {
            foreach (var replacement in replacements)
            {
                var text = Encoding.ASCII.GetBytes(replacement.text);
                fileStream.Position = replacement.start;
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
            }
        }

        public static IEnumerable<Replacement> FindReplacements(FileStream fileStream)
        {
            // Number of bytes to read from the file at a time. This should exceed the max length of the patterns to search for.
            var readSize = 4096;
            // Buffer is twice as large as the read, to account for matches spanning two reads
            var buffer = new byte[readSize * 2];
            // Because we push into the buffer from right to left
            var bufferToPositionOffset = -buffer.Length;
            var replacements = new List<Replacement>();

            while (!(fileStream.Position >= fileStream.Length))
            {
                // Shift the buffer to the left by half its length
                Array.Copy(buffer, readSize, buffer, 0, readSize);

                // Append new data to the second half of the buffer
                var bytesRead = fileStream.Read(buffer, readSize, readSize);

                // Update the offset to reflect the shift made above
                bufferToPositionOffset += readSize;

                // Read the left half of the buffer, plus whatever was just read in (this avoids end of file issues)
                var chunk = Encoding.ASCII.GetString(buffer, 0, readSize + bytesRead);
                replacements.AddRange(GetDateReplacements(chunk, bufferToPositionOffset));
                replacements.AddRange(GetIdReplacements(chunk, bufferToPositionOffset));
            }

            // De-dupe because some matches might occur in both the left and right sides of the buffer
            return replacements
                .GroupBy(x=>x.start)
                .Select(x=>x.First());
        }

        static IEnumerable<Replacement> GetDateReplacements(string input, long positionOffset)
        {
            // This would be a cleaner value, but would represent a breaking change because people might already be successfully approving with the old arbitrary valu
            // var scrubbedDateTemplate = "19000101000000+00'00'";

            var scrubbedDateTemplate = "20110426104115-07'00'";

            return FindDates(input)
                .Select(pos => new Replacement
                {
                    start = positionOffset + pos.start,
                    text = scrubbedDateTemplate.Substring(0, pos.length)
                });
        }

        static IEnumerable<Replacement> GetIdReplacements(string input, long positionOffset)
        {
            return FindIds(input)
                .Select(pos => new Replacement{start = positionOffset + pos.start,text = new string('0', pos.length)});
        }

        public static IEnumerable<Id> FindDates(string input)
        {
            // PDF Date format is at least (D:YYYY), but can be as long as (D:YYYYMMDDHHmmSSOHH'mm'), where O can be Z, + or -. Chars after the O denote offset.
            // "Closely follow that of the international standard ASN.1 (Abstract Syntax Notation One), defined in ISO/IEC 8824."

            var regex = new Regex(@"(?x)  # Allow comments and ignore whitespace
                \(D:                    # Denotes the start of date metadata
                (                       # Open Group 1: Main capturing group that we are interested in
                    \d{4}               # Mandatory 4 digit year (YYYY)
                    ([0-1]\d)?          # Group 2: Month (MM). Optional.
                    (?(2)([0-3]\d)?)    # Group 3: Day (DD). Optional, only match if previous group matches.
                    (?(3)([0-2]\d)?)    # Group 4: Optional hour (HH). Optional, only match if previous group matches.
                    (?(4)([0-5]\d)?)    # Group 5: Optional minutes (mm). Optional, only match if previous group matches.
                    (?(5)([0-5]\d)?)    # Group 6: Optional seconds (SS). Optional, only match if previous group matches.
                    (?(6)([Z+-])?)      # Group 7: Optional offset (Z, -, +). Optional, only match if previous group matches.
                    (?(7)([0-2]\d)?)    # Group 8: Optional offset hours (HH). Optional, only match if previous group matches.
                    (?(8)(')?)          # Group 9: Optional offset delimiter ('). Optional, only match if previous group matches.
                    (?(9)([0-5]\d)?)    # Group 10: Optional offset minutes (mm). Optional, only match if previous group matches.
                    (?(10)'?)           # Optional offset delimiter (') Optional, only match if previous group matches.
                )                       # Close Group 1
                \)                      # End of date metadata
            ");

            var matches = regex.Matches(input);
            return matches
                .OfType<Match>()
                .Select(match => new Id{start = match.Groups[1].Index,length = match.Groups[1].Length});
        }

        public class Id
        {
            public int start;
            public int length;

            public override string ToString()
            {
                return $"{nameof(start)}: {start}, {nameof(length)}: {length}";
            }
        }

        public static IEnumerable<Id> FindIds(string input)
        {
            // File identifiers are defined by the optional /ID entry in a PDF file's trailer dictionary.
            // The spec calls for an array of two strings. Although it recommends using an md5 hash to generate them, it does not demand them.

            // Match the pattern:
            //
            // trailer
            // << /ID [ < string1 >< string2 > ] >>
            //
            // allowing for other entries and whitespace

            var regex = new Regex(@"(?x)  # Allow comments and ignore whitespace
                trailer     # Declare the trailer dictionary.
                \s+         # Newline and optional spaces
                <<          # Begin trailer dictionary entries
                .*          # Allow for other entries in the trailer dictionary that precede the ID entry
                \/ID        # Declare the the /ID entry
                \s*         # Optional whitespace
                \[          # Begin array of ID values
                \s*         # Optional whitespace
                <(.*)>      # Group 1: First ID value, any string enclosed in <>
                \s*         # Optional whitespace
                <(.*)>      # Group 2: Second ID value, any string enclosed in <>
                \s*         # Optional whitespace
                \]          # End array of ID values
                .*          # Allow for other entries in the trailer dictionary that succeed the ID entry
                >>          # End trailer dictionary entries
            ");

            var match = regex.Match(input);
            if (match.Groups.Count == 3)
            {
                return match.Groups.OfType<Group>()
                    .Skip(1) // Skip the first group which contains the entire match
                    .Select(group => new Id{start = group.Index, length = group.Length});
            }

            return new List<Id>();
        }
    }
}