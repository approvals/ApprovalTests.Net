using ApprovalTests.Core;

namespace ApprovalTests.Tests.Reporters
{
    public class RecordingReporter : IEnvironmentAwareReporter
    {
        private readonly bool working;

        public RecordingReporter()
        {
            working = true;
        }

        public RecordingReporter(bool working)
        {
            this.working = working;
        }

        public void Report(string approved, string received)
        {
            CalledWith = $"{approved},{received}";
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return working;
        }

        public string CalledWith { get; set; }
    }
}