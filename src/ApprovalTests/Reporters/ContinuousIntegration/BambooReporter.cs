namespace ApprovalTests.Reporters.ContinuousIntegration;

public class BambooReporter : IEnvironmentAwareReporter
{
    public static readonly BambooReporter INSTANCE = new();

    public void Report(string approved, string received) =>
        ContinuousDeliveryUtils.ReportOnServer(approved, received);

    public bool IsWorkingInThisEnvironment(string forFile) =>
        Environment.GetEnvironmentVariable("bamboo_buildNumber") != null;
}