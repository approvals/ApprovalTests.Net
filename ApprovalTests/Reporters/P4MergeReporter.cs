namespace ApprovalTests.Reporters
{
	public class P4MergeReporter : FirstWorkingReporter
	{
		public static readonly string PATH = DotNet4Utilities.GetPathInProgramFilesX86("Perforce" + System.IO.Path.DirectorySeparatorChar + "p4merge.exe");
		public static readonly P4MergeReporter INSTANCE = new P4MergeReporter();

		public P4MergeReporter()
			: base(P4MergeTextReporter.INSTANCE, P4MergeImageReporter.INSTANCE)	
		{
		}

	}
}