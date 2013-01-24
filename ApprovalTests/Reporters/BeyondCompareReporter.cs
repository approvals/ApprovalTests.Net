using ApprovalUtilities.Utilities;
using System.Linq;

namespace ApprovalTests.Reporters
{
	public class BeyondCompareReporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"Beyond Compare 3\BCompare.exe");
		public readonly static BeyondCompareReporter INSTANCE = new BeyondCompareReporter();

		public BeyondCompareReporter()
			: base(PATH, GenericDiffReporter.DEFAULT_ARGUMENT_FORMAT,
            "Could not find BeyondCompare at {0}, please install it".FormatWith(PATH),
            GenericDiffReporter.TEXT_FILE_TYPES.Concat(TortoiseImageDiffReporter.IMAGE_FILE_TYPES).ToArray())
		{

		}
	}
}