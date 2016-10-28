namespace ApprovalTests.Reporters
{
    public class P4MergeImageReporter : GenericDiffReporter
    {
        public static readonly P4MergeImageReporter INSTANCE = new P4MergeImageReporter();

        public P4MergeImageReporter()
            : base(DiffPrograms.Windows.P4MERGE_IMAGE)
        {
        }
    }
}