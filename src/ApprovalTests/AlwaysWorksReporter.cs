using ApprovalTests.Core;

namespace ApprovalTests
{
    internal class AlwaysWorksReporter : IEnvironmentAwareReporter
    {
        private readonly IApprovalFailureReporter reporter;

        public AlwaysWorksReporter(IApprovalFailureReporter reporter)
        {
            this.reporter = reporter;
        }


        public void Report(string approved, string received)
        {
            reporter.Report(approved, received);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return true;
        }

    }
}