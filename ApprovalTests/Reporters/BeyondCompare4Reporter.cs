using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class BeyondCompare4Reporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"Beyond Compare 4" + System.IO.Path.DirectorySeparatorChar + "BCompare.exe");
		public readonly static BeyondCompare4Reporter INSTANCE = new BeyondCompare4Reporter();

		public BeyondCompare4Reporter()
			: base(PATH, GenericDiffReporter.DEFAULT_ARGUMENT_FORMAT,
            "Could not find BeyondCompare at {0}, please install it".FormatWith(PATH),
            BeyondCompare3Reporter.ImageAndTextFiles)
		{

		}

	}
}