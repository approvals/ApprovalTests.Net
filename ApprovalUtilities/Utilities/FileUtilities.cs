using System.IO;

namespace ApprovalUtilities.Utilities
{
	public class FileUtilities
	{
		public static void EnsureFileExists(string approved)
		{
			if (!File.Exists(approved))
			{
				File.Create(approved).Dispose();
			}
		}
	}
}