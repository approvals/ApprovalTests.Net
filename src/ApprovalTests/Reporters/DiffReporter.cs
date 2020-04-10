using ApprovalTests.Core;
using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class DiffReporter : IEnvironmentAwareReporter
    {
        public static readonly DiffReporter INSTANCE = new DiffReporter();

        public void Report(string approved, string received)
        {
            DiffRunner.Launch(received, approved);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return true;
        }
    }
}