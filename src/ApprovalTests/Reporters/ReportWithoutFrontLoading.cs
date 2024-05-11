namespace ApprovalTests.Reporters;

public class ReportWithoutFrontLoading : IEnvironmentAwareReporter
{
    public static ReportWithoutFrontLoading INSTANCE = new();
    public void Report(string approved, string received)
    {
        // do nothing
    }

    public bool IsWorkingInThisEnvironment(string forFile) => false;
}