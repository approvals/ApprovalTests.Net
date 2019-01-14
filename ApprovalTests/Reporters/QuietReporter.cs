using System;
using System.Diagnostics;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    /// <summary>
    /// Reporter that print the command line to accept all the test results
    /// </summary>
    public class QuietReporter : IEnvironmentAwareReporter
    {
        public static readonly QuietReporter INSTANCE = new QuietReporter();

        public void Report(string approved, string received)
        {
            DisplayCommandLineApproval(approved, received);
        }

        public bool ShouldIgnoreLineEndings { get; set; }

        public static void DisplayCommandLineApproval(string approved, string received)
        {
            var message = GetCommandLineForApproval(approved, received);
            Debug.WriteLine(message);
            Console.WriteLine(message);
        }

        public static string GetCommandLineForApproval(string approved, string received)
        {
            return $"cmd /c move /Y \"{received}\" \"{approved}\"";
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return true;
        }
    }
}