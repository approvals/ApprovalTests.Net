using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class P4MergeImageReporter : GenericDiffReporter
	{
		public static readonly P4MergeImageReporter INSTANCE = new P4MergeImageReporter();

		public P4MergeImageReporter()
			: base(P4MergeReporter.PATH, DEFAULT_ARGUMENT_FORMAT, "Could not find P4Merge at {0}, please install it".FormatWith(P4MergeReporter.PATH), GetImageFileTypes)
		{
		}
	}
}