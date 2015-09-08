namespace ApprovalTests.Reporters
{

	public class KDiffReporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"KDiff3" + System.IO.Path.DirectorySeparatorChar + "kdiff3.exe");
		public static readonly KDiffReporter INSTANCE = new KDiffReporter();
		public KDiffReporter()
			: base(PATH, "Please install KDIFF3")
		{

		}
	}

}