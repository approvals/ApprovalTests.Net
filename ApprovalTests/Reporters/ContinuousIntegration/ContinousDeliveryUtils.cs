using ApprovalTests.Reporters.TestFrameworks;

namespace ApprovalTests.Reporters.ContinuousIntegration
{
    public class ContinousDeliveryUtils
    {
        public static void ReportOnServer(string approved, string received)
        {
            var reporter = FrameworkAssertReporter.INSTANCE;
            if (reporter.IsWorkingInThisEnvironment(received))
            {
                reporter.Report(approved, received);
            }
            else
            {
                // do nothing
            }
        }
    }
}