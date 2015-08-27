using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class TortoiseTextDiffReporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"TortoiseSVN" + System.IO.Path.DirectorySeparatorChar + "bin" + System.IO.Path.DirectorySeparatorChar + "tortoisemerge.exe");
		public readonly static TortoiseTextDiffReporter INSTANCE = new TortoiseTextDiffReporter();

		public TortoiseTextDiffReporter()
			: base(
				PATH,
				"Could not find TortoiseMerge at {0}, please install it (it's part of TortoiseSVN) http://tortoisesvn.net/ ".
					FormatWith(PATH))
		{
		}
	}
}