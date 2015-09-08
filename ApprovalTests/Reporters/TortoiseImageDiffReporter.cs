using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class TortoiseImageDiffReporter : GenericDiffReporter
	{
		readonly static string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"TortoiseSVN" + System.IO.Path.DirectorySeparatorChar + "bin" + System.IO.Path.DirectorySeparatorChar + "TortoiseIDiff.exe");
		public readonly static TortoiseImageDiffReporter INSTANCE = new TortoiseImageDiffReporter();

		public TortoiseImageDiffReporter()
			: base(
				PATH, "/left:\"{0}\" /right:\"{1}\"",
				"Could not find TortoiseMerge at {0}, please install it (it's part of TortoiseSVN) http://tortoisesvn.net/ ".
					FormatWith(PATH), GetImageFileTypes)
		{
		}
		
	}
}