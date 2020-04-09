using ApprovalTests.Core;
using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class DiffEngineReporter : IEnvironmentAwareReporter
    {
        DiffTool diffTool;

        public DiffEngineReporter(DiffTool diffTool)
        {
            this.diffTool = diffTool;
        }
        public void Report(string approved, string received)
        {
            DiffRunner.Launch(diffTool, received, approved);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return DiffTools.IsDetectedFor(diffTool,forFile);
        }
    }
}