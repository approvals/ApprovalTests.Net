namespace ApprovalTests.Reporters;

public class DiffToolReporter(DiffTool diffTool) : IEnvironmentAwareReporter
{
    public void Report(string approved, string received) =>
        DiffRunner.Launch(diffTool, received, approved);

    public bool IsWorkingInThisEnvironment(string forFile) =>
        DiffTools.IsDetectedForFile(diffTool, forFile);
}