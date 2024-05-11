namespace ApprovalTests.Reporters.ContinuousIntegration;

public class TeamCityReporter : IEnvironmentAwareReporter
{
    public static readonly TeamCityReporter INSTANCE = new();

    public void Report(string approved, string received) =>
        ContinuousDeliveryUtils.ReportOnServer(approved, received);

    public bool IsWorkingInThisEnvironment(string forFile) =>
        Environment.GetEnvironmentVariable("TEAMCITY_VERSION") != null;
}