using System;
using System.Text;
using Alphaleonis.Win32.Filesystem;
using ApprovalTests.Core;

namespace ApprovalTests
{
    public class ApprovalTextWriter : IApprovalWriter
    {
        public ApprovalTextWriter(string data) : this(data, "txt")
        {
            Data = data;
        }

        public ApprovalTextWriter(string data, string extensionWithoutDot)
        {
            Data = data;
            ExtensionWithDot = EnsureDot(extensionWithoutDot);
        }

        public static string EnsureDot(string extension)
        {
            var extensionWithDot = $".{extension}";
            return extension.StartsWith(".") ? extension : extensionWithDot;
        }

        public string Data { get; set; }
        public string ExtensionWithDot { get; set; }


        public virtual string GetApprovalFilename(string basename)
        {
            return $"{basename}.approved{ExtensionWithDot}";
        }

        public virtual string GetReceivedFilename(string basename)
        {
            return $"{basename}.received{ExtensionWithDot}";
        }

        public string WriteReceivedFile(string received)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(received));
            File.WriteAllText(received, Data, Encoding.UTF8);
            DoUpgradeToUTF8Patch(received);
            return received;
        }

        private void DoUpgradeToUTF8Patch(string received)
        {
            var approved = received.Replace(".received", ".approved");
            if (File.Exists(approved) && !IsUft8ByteOrderMarkPresent(approved))
            {
                ConsoleUtilities.WriteLine($"Upgrading {approved} to include Utf8 Byte Order Mark. (this is a 1 time event)");
                var text = File.ReadAllText(approved);
                File.WriteAllText(approved, text, Encoding.UTF8);
            }
        }


        public static bool IsUft8ByteOrderMarkPresent(string file)
        {
            var preamble = Encoding.UTF8.GetPreamble();
            var readAllBytes = ReadBytes(file, preamble.Length);
            if (readAllBytes.Length < preamble.Length)
            {
                return false;
            }

            for (var i = 0; i < preamble.Length; i++)
            {
                if (preamble[i] != readAllBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] ReadBytes(string file, int length)
        {
            byte[] buffer;
            using (var fileStream = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                var offset = 0;
                var fileLength = fileStream.Length;
                var count = (int) Math.Min(length, fileLength);
                buffer = new byte[count];
                while (count > 0)
                {
                    var num = fileStream.Read(buffer, offset, count);
                    if (num == 0)
                    {
                        throw new Exception("Unexpected End of File while reading " + file);
                    }

                    offset += num;
                    count -= num;
                }
            }

            return buffer;
        }
    }
}