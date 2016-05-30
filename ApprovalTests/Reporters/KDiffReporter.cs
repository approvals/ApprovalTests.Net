namespace ApprovalTests.Reporters
{

	public class KDiffReporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"KDiff3" + System.IO.Path.DirectorySeparatorChar + "kdiff3.exe");
		public static readonly KDiffReporter INSTANCE = new KDiffReporter();
		public KDiffReporter()
            : base(PATH, "\"{0}\" \"{1}\" -m -o \"{1}\"", "Please install KDIFF3")
		{

		}
	}

}
