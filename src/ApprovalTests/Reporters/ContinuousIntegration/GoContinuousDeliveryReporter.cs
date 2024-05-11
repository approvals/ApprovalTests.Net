namespace ApprovalTests.Reporters.ContinuousIntegration;

public class GoContinuousDeliveryReporter : IEnvironmentAwareReporter
{
    public static readonly GoContinuousDeliveryReporter INSTANCE = new();

    public void Report(string approved, string received) =>
        ContinuousDeliveryUtils.ReportOnServer(approved, received);

    public bool IsWorkingInThisEnvironment(string forFile) =>
        Environment.GetEnvironmentVariable("GO_SERVER_URL") != null;
}