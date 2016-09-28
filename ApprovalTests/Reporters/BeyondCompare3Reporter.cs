namespace ApprovalTests.Reporters
{
    public class BeyondCompare3Reporter : GenericDiffReporter
    {
        public static readonly BeyondCompare3Reporter INSTANCE = new BeyondCompare3Reporter();

        public BeyondCompare3Reporter()
            : base(DiffPrograms.Windows.BEYOND_COMPARE_3)
        {
        }
    }
}