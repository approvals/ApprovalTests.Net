using System;
using System.IO;

namespace ApprovalTests.Scrubber
{
	public class PdfScrubber
	{
		public static void ScrubPdf(string pdfFilePath)
		{
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
					var desired = new System.Text.ASCIIEncoding().GetBytes(original);

					pdf.Write(desired, 0, desired.Length);
					pdf.Flush();
				}
			}
		}
		public  static long Find(string token, Stream fileStream)
		{
			while (fileStream.Length != fileStream.Position)
			{
				if (Compare(token[0], fileStream.ReadByte()))
				{
					var location = fileStream.Position - 1;
					bool fail = false;
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