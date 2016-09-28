using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class AppVeyorReporter : IEnvironmentAwareReporter
    {
        public static readonly AppVeyorReporter INSTANCE = new AppVeyorReporter();

        public void Report(string approved, string received)
        {
            ContinousDeliveryUtils.ReportOnServer(approved,received);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            var flag = Environment.GetEnvironmentVariable("APPVEYOR");
            return "True".Equals(flag, StringComparison.OrdinalIgnoreCase);
        }
    }
}