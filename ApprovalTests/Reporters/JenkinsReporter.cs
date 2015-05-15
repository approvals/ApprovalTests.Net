using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class JenkinsReporter : IEnvironmentAwareReporter
    {
        public static readonly JenkinsReporter INSTANCE = new JenkinsReporter();

        public void Report(string approved, string received)
        {
            // does nothing
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("JENKINS_URL") != null;
        }
    }
}