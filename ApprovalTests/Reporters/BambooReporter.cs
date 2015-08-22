using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class BambooReporter : IEnvironmentAwareReporter
    {
        public static readonly BambooReporter INSTANCE = new BambooReporter();

        public void Report(string approved, string received)
        {
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("bamboo_buildNumber") != null;
        }
    }
}