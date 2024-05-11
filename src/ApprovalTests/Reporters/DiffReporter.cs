namespace ApprovalTests.Reporters;

public class DiffReporter : IEnvironmentAwareReporter
{
    public static readonly DiffReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        var launch = DiffRunner.Launch(received, approved);
        if (launch == LaunchResult.NoDiffToolFound)
        {
            throw new($"Could not find a diff tool for extension: {Path.GetExtension(received)}");
        }
    }

    public bool IsWorkingInThisEnvironment(string forFile) => true;
}