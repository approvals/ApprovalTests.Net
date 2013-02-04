using System.IO;
using System.Text;

namespace ApprovalUtilities.Utilities
{
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
			using (var sr = new StreamReader(file, true))
			{
				for (int i= 0; i < 4 && sr.Peek() >= 0; i++)
				{
					sr.Read();
				}

				return sr.CurrentEncoding;
			}
		}
	}
}

