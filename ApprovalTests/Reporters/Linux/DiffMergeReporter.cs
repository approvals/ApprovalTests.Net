namespace ApprovalTests.Reporters.Linux
{
    public class DiffMergeReporter : GenericDiffReporter
    {

        public static readonly DiffMergeReporter INSTANCE = new DiffMergeReporter();

        public DiffMergeReporter() : base(DiffPrograms.Linux.DIFF_MERGE)
        {

        }
    }


}