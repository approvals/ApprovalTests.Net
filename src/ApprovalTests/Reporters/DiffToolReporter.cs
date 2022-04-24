using ApprovalTests.Core;
using DiffEngine;

namespace ApprovalTests.Reporters;

public class DiffToolReporter : IEnvironmentAwareReporter
{
    DiffTool diffTool;

    public DiffToolReporter(DiffTool diffTool)
    {
        this.diffTool = diffTool;
    }

    public void Report(string approved, string received)
    {
        DiffRunner.Launch(diffTool, received, approved);
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        return DiffTools.IsDetectedFor(diffTool, forFile);
    }
}