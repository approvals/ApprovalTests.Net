namespace ApprovalTests.Reporters.TestFrameworks;

public class FrameworkAssertReporter : FirstWorkingReporter
{
    public static readonly FrameworkAssertReporter INSTANCE = new();

    public FrameworkAssertReporter()
        : base(
            MsTestReporter.INSTANCE,
            NUnit3Reporter.INSTANCE,
            NUnit4Reporter.INSTANCE,
            XUnit2Reporter.INSTANCE)
    {
    }
}