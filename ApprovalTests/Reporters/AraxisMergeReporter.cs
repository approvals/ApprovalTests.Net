using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class AraxisMergeReporter : GenericDiffReporter
	{
		private static readonly string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"Araxis\Araxis Merge\Compare.exe");
		public static readonly AraxisMergeReporter INSTANCE = new AraxisMergeReporter();

		public AraxisMergeReporter()
			: base(
				PATH,
				"\"{0}\" \"{1}\"",
				"Could not find Araxis Merge at {0}.".FormatWith(PATH))
		{
		}
	}
}