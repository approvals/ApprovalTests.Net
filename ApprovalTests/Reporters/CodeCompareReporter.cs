using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class CodeCompareReporter : GenericDiffReporter
	{
		private readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"Devart" + System.IO.Path.DirectorySeparatorChar + "Code Compare" + System.IO.Path.DirectorySeparatorChar + "CodeCompare.exe");
		public readonly static CodeCompareReporter INSTANCE = new CodeCompareReporter();

		public CodeCompareReporter()
			: base(
			PATH,
			"/ENVIRONMENT=visualstudio \"{0}\" \"{1}\"",
			"Could not find DevArt Code Compare at {0}.".FormatWith(PATH))
		{
		}
	}
}