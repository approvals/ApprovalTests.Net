using System.Collections.Generic;
using ApprovalUtilities.Utilities;
using System.Linq;

namespace ApprovalTests.Reporters
{
	public class BeyondCompare3Reporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"Beyond Compare 3" + System.IO.Path.DirectorySeparatorChar + "BCompare.exe");
		public readonly static BeyondCompare3Reporter INSTANCE = new BeyondCompare3Reporter();

		public BeyondCompare3Reporter()
			: base(PATH, GenericDiffReporter.DEFAULT_ARGUMENT_FORMAT,
            "Could not find BeyondCompare at {0}, please install it".FormatWith(PATH),
            ImageAndTextFiles)
		{

		}

		public static IEnumerable<string> ImageAndTextFiles()
		{
			return GetTextFileTypes().Concat(GetImageFileTypes());
		}
	}
}