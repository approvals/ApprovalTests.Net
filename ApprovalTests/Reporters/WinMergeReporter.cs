using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class WinMergeReporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"WinMerge" + System.IO.Path.DirectorySeparatorChar + "WinMergeU.exe");
		public static readonly WinMergeReporter INSTANCE = new WinMergeReporter();
		public WinMergeReporter()
			: base(PATH, "Could not find WinMerge at {0}, please install it".FormatWith(PATH))
		{
		}
	}
}