using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class P4MergeTextReporter : GenericDiffReporter
	{
		public static readonly P4MergeTextReporter INSTANCE = new P4MergeTextReporter();

		public P4MergeTextReporter()
			: base(P4MergeReporter.PATH, "\"{1}\" \"{0}\" \"{1}\" \"{1}\"", "Could not find P4Merge at {0}, please install it".FormatWith(P4MergeReporter.PATH))
		{
		}
	}
}