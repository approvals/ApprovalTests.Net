using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters.ContinuousIntegration
{
    public class GoContinuousDeliveryReporter : IEnvironmentAwareReporter
    {
        public static readonly GoContinuousDeliveryReporter INSTANCE = new GoContinuousDeliveryReporter();

        public void Report(string approved, string received)
        {
            ContinousDeliveryUtils.ReportOnServer(approved, received);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("GO_SERVER_URL") != null;
        }
    }
}