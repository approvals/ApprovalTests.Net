using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApprovalTests.Scrubber
{
    public static class PdfScrubber
    {
        public static void ScrubPdf(string pdfFilePath)
        {
            using (var fileStream = File.Open(pdfFilePath, FileMode.Open))
            {
                ScrubDates(fileStream);
                ScrubId(fileStream);
            }



            long location;
            using (var pdf = File.OpenRead(pdfFilePath))
            {
                location = Find("/CreationDate (", pdf);
            }

            if (0 <= location)
            {
                using (var pdf = File.OpenWrite(pdfFilePath))
                {
                    pdf.Seek(location, SeekOrigin.Begin);

                    var original = "/CreationDate (D:20110426104115-07'00')";
                    var desired = new ASCIIEncoding().GetBytes(original);

                    pdf.Write(desired, 0, desired.Length);
                    pdf.Flush();
                }
            }
        }

        private static void ScrubId(FileStream fileStream)
        {
            throw new NotImplementedException();
        }

        private static void ScrubDates(FileStream fileStream)
        {
            // PDF Date format is at least (D:YYYY), but can be as long as (D:YYYYMMDDHHmmSSOHH'mm'), where O can be Z, + or -. Chars after the 0 denote offset.
            // "Closely follow that of the international standard ASN.1 (Abstract Syntax Notation One), defined in ISO/IEC 8824."

            // Formats
            var oldSpec = "yyyyMMddhhmmsszzzz:";
            var fullOffset = "yyyyMMddhhmmsszzzz";
            var hourOffset = "yyyyMMddhhmmsszz";
            var utcMarker = "yyyyMMddhhmmssK";
            var noOffset = "yyyyMMddhhmmss";


        }

        private static IEnumerable<(long, long)> FindMatches(FileStream fileStream)
        {
            var bytePattern = new []{40,68,58}.Select(Convert.ToByte).ToArray(); // Represents the string "(D:", our match pattern
            var buffer = new byte[8192]; // Buffer is twice as large as the read
            var matches = new List<(long, long)>();
            while (fileStream.Length != fileStream.Position)
            {
                // Shift the buffer to the left by half its length
                Array.Copy(buffer, 4096, buffer, 0, 4096);

                // Append new data to the second half of the buffer
                fileStream.Read(buffer, 4096, 4096);

                // Look for string
                for (var i = 0; i < buffer.Length; i++)
                {
                    if (!IsMatch(buffer, i, bytePattern))
                    {
                        continue;
                    }

                    var end = FindEnd(buffer, i);
                    if (end > -1)
                    {
                        var text = Enumerable.Range(i, end - i)
                        VerifyDate()
                    }
                }
            }
        }

        private static int FindEnd(byte[] buffer, int start)
        {
            var endBytes = new[] { 41 }.Select(Convert.ToByte).ToArray();

            for (var i = start; i <= 25; i++)
            {
                if (IsMatch(buffer, i, endBytes))
                {
                    return i;
                }
            }

            return -1;
        }

        // Cheers to old mate: https://stackoverflow.com/a/283648/866359

        private static bool IsMatch(byte[] bytes, long start, byte[] searchPattern)
        {
            if (searchPattern.Length > bytes.Length - start)
            {
                return false;
            }

            for (var i = 0; i < searchPattern.Length; i++)
            {
                if (bytes[start + i] != searchPattern[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static long Find(string token, Stream fileStream)
        {
            while (fileStream.Length != fileStream.Position)
            {
                if (Compare(token[0], fileStream.ReadByte()))
                {
                    var location = fileStream.Position - 1;
                    var fail = false;
                    for (var index = 1; index <= token.Length - 1; index++)
                    {
                        if (!Compare(token[index], fileStream.ReadByte()))
                        {
                            fail = true;
                            break;
                        }

                    }

                    if (!fail)
                    {
                        return location;
                    }
                }
            }

            return -1L;
        }

        private static bool Compare(char c, int i)
        {
            return Convert.ToChar(i) == c;
        }
    }
}