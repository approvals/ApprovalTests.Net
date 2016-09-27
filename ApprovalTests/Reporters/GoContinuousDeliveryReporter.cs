using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class GoContinuousDeliveryReporter : IEnvironmentAwareReporter
    {
        public static readonly GoContinuousDeliveryReporter INSTANCE = new GoContinuousDeliveryReporter();

        public void Report(string approved, string received)
        {
            reportOnServer(approved, received);
        }

        public static void reportOnServer(string approved, string received)
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

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("GO_SERVER_URL") != null;
        }
    }

    public class ContinousDeliveryUtils
    {
    }
}