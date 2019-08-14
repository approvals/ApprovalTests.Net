namespace ApprovalTests.Reporters.Windows
{
    public class BeyondCompareReporter : FirstWorkingReporter
    {
        public static readonly BeyondCompareReporter INSTANCE = new BeyondCompareReporter();

        public BeyondCompareReporter()
            : base(BeyondCompare4Reporter.INSTANCE, BeyondCompare3Reporter.INSTANCE)
        {
        }
    }
}