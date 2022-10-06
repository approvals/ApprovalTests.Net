namespace ApprovalTests.Reporters.ContinuousIntegration;

public class JenkinsReporter : IEnvironmentAwareReporter
{
    public static readonly JenkinsReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        ContinuousDeliveryUtils.ReportOnServer(approved, received);
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        return Environment.GetEnvironmentVariable("JENKINS_URL") != null;
    }
}