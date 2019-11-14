namespace ApprovalTests.Reporters.Mac
{
    public class P4MergeReporter : GenericDiffReporter
    {

        public static readonly P4MergeReporter INSTANCE = new P4MergeReporter();

        public P4MergeReporter() : base(DiffPrograms.Mac.P4MERGE)
        {

        }
    }
}