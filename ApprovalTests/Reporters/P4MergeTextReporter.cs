namespace ApprovalTests.Reporters
{
    public class P4MergeTextReporter : GenericDiffReporter
    {
        public static readonly P4MergeTextReporter INSTANCE = new P4MergeTextReporter();

        public P4MergeTextReporter()
            : base(DiffPrograms.Windows.P4MERGE_TEXT)
        {
        }
    }
}