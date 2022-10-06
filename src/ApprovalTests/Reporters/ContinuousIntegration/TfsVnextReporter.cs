namespace ApprovalTests.Reporters.ContinuousIntegration;

public class TfsVnextReporter : IEnvironmentAwareReporter
{
    public static readonly TfsVnextReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        ContinuousDeliveryUtils.ReportOnServer(approved, received);
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        return Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT") != null;
    }
}