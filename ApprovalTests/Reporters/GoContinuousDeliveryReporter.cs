using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class GoContinuousDeliveryReporter : IEnvironmentAwareReporter
    {
        public static readonly GoContinuousDeliveryReporter INSTANCE = new GoContinuousDeliveryReporter();

        public void Report(string approved, string received)
        {
            // does nothing
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("GO_SERVER_URL") != null;
        }
    }
}