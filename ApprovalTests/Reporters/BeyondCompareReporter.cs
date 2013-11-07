using System.Collections.Generic;
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
            ImageAndTextFiles)
		{

		}

		private static IEnumerable<string> ImageAndTextFiles()
		{
			return GetTextFileTypes().Concat(GetImageFileTypes());
		}
	}
}