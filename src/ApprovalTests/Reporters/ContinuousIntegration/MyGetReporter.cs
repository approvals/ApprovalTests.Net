using ApprovalTests.Core;

namespace ApprovalTests.Reporters.ContinuousIntegration;

public class MyGetReporter : IEnvironmentAwareReporter
{
    public static readonly MyGetReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        // does nothing
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        return "MyGet".Equals(Environment.GetEnvironmentVariable("BuildRunner"));
    }
}