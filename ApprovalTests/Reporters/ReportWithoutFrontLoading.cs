using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class ReportWithoutFrontLoading : IEnvironmentAwareReporter
    {
        public static ReportWithoutFrontLoading INSTANCE = new ReportWithoutFrontLoading();
        public void Report(string approved, string received)
        {
            // do nothing
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return false;
        }
    }
}