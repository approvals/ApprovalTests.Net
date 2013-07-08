using System.IO;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class TortoiseComboImageReporter : IEnvironmentAwareReporter
	{
		public static TortoiseComboImageReporter INSTANCE = new TortoiseComboImageReporter();
		public void Report(string approved, string received)
		{
			ClipboardReporter.INSTANCE.Report(approved, received);

			if (FileExistsAndNonEmpty(approved))
			{
				TortoiseImageDiffReporter.INSTANCE.Report(approved, received);
			}
			else
			{
				FileLauncherReporter.INSTANCE.Report(approved, received);
			}
		}

		public static bool FileExistsAndNonEmpty(string file)
		{
			var a = new FileInfo(file);
			return a.Exists && a.Length > 50;
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return TortoiseImageDiffReporter.INSTANCE.IsWorkingInThisEnvironment(forFile);
		}
	}
}