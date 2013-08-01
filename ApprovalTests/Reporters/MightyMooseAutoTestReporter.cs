using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class MightyMooseAutoTestReporter : IEnvironmentAwareReporter
    {
        public static readonly MightyMooseAutoTestReporter INSTANCE = new MightyMooseAutoTestReporter();

        public static bool? IsRunning = null;

        public void Report(string approved, string received)
        {
            // do nothing
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            if (IsRunning == null)
            {
                var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                IsRunning = processName != null && processName.StartsWith("AutoTest.TestRunner");
            }

            return IsRunning.Value;
        }
    }
}