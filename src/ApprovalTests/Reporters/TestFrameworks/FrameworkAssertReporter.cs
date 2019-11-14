namespace ApprovalTests.Reporters.TestFrameworks
{
    public class FrameworkAssertReporter : FirstWorkingReporter
    {
        public static readonly FrameworkAssertReporter INSTANCE = new FrameworkAssertReporter();

        public FrameworkAssertReporter()
            : base(MsTestReporter.INSTANCE,
                NUnitReporter.INSTANCE,
                XUnit2Reporter.INSTANCE)
        {
        }
    }
}