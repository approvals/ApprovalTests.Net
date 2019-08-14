namespace ApprovalTests.Reporters.Windows
{
    public class WinMergeReporter : GenericDiffReporter
    {
        public static readonly WinMergeReporter INSTANCE = new WinMergeReporter();

        public WinMergeReporter()
            : base(DiffPrograms.Windows.WIN_MERGE)
        {
        }
    }
}