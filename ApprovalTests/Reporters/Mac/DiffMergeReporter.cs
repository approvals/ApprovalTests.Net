namespace ApprovalTests.Reporters.Mac
{
    public class DiffMergeReporter : GenericDiffReporter
    {

        public static readonly DiffMergeReporter INSTANCE = new DiffMergeReporter();

        public DiffMergeReporter() : base(DiffPrograms.Mac.DIFF_MERGE)
        {

        }
    }


}