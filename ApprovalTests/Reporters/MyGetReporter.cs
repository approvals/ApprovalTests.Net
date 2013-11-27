using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class MyGetReporter : IEnvironmentAwareReporter
    {
        public static readonly MyGetReporter INSTANCE = new MyGetReporter();

        public void Report(string approved, string received)
        {
            // does nothing
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return "MyGet".Equals(Environment.GetEnvironmentVariable("BuildRunner"));
        }
    }
}