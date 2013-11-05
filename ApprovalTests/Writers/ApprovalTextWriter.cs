using System;
using System.IO;
using System.Text;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

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
			string extensionWithDot = String.Format(".{0}", extension);
			return extension.StartsWith(".") ? extension : extensionWithDot;
		}

		public string Data { get; set; }
		public string ExtensionWithDot { get; set; }


		public virtual string GetApprovalFilename(string basename)
		{
			return String.Format("{0}.approved{1}", basename, ExtensionWithDot);
		}

		public virtual string GetReceivedFilename(string basename)
		{
			return String.Format("{0}.received{1}", basename, ExtensionWithDot);
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
				ConsoleUtilities.WriteLine("Upgrading {0} to include Utf8 Byte Order Mark. (this is a 1 time event)".FormatWith(approved));
				var text = File.ReadAllText(approved);
				File.WriteAllText(approved, text, Encoding.UTF8);
			}
		}


		public static bool IsUft8ByteOrderMarkPresent(string file)
		{
			byte[] preamble = Encoding.UTF8.GetPreamble();
			byte[] readAllBytes = ReadBytes(file, preamble.Length);
			if (readAllBytes.Length < preamble.Length)
			{
				return false;
			}
			for (int i = 0; i < preamble.Length; i++)
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
			using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				int offset = 0;
				long fileLength = fileStream.Length;
				int count = (int) Math.Min(length, fileLength);
				buffer = new byte[count];
				while (count > 0)
				{
					int num = fileStream.Read(buffer, offset, count);
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