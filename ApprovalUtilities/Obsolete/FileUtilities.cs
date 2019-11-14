using System.IO;
using System.Text;

namespace ApprovalUtilities.Utilities
{
    [ObsoleteEx(
        RemoveInVersion = "5.0")]
    public class FileUtilities
    {
        public static void EnsureFileExists(string file)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, " ", Encoding.UTF8);
            }
        }

        public static void EnsureFileExistsAndMatchesEncoding(string file, string matchEncodingFrom)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, " ", GetEncodingFor(matchEncodingFrom));
            }
        }

        public static Encoding GetEncodingFor(string file)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                stream.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76)
            {
                return Encoding.UTF7;
            }

            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                return Encoding.UTF8;
            }

            if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                //UTF-16LE
                return Encoding.Unicode;
            }

            if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                //UTF-16BE
                return Encoding.BigEndianUnicode;
            }

            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
            {
                return Encoding.UTF32;
            }

            return Encoding.ASCII;
        }
    }
}