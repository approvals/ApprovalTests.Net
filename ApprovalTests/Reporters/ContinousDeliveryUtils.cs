namespace ApprovalTests.Reporters
{
    public class ContinousDeliveryUtils
    {
        public static void ReportOnServer(string approved, string received, bool shouldIgnoreLineEndings)
        {
            var reporter = FrameworkAssertReporter.INSTANCE;
            reporter.ShouldIgnoreLineEndings = shouldIgnoreLineEndings;

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