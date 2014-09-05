using ApprovalTests.Utilities;

namespace ApprovalTests.Reporters
{
    using System;
    using System.Diagnostics;

    using ApprovalTests.Core;

    public class TfsReporter : IEnvironmentAwareReporter
    {
        public static readonly TfsReporter INSTANCE = new TfsReporter();

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return "TFSBuildServiceHost".Equals(GetParentProcessName());
        }

        public void Report(string approved, string received)
        {
            // does nothing
        }

        private static string GetParentProcessName()
        {
            var parentProcess = ParentProcessUtils.GetParentProcess(Process.GetCurrentProcess());
            return parentProcess == null ? string.Empty : parentProcess.ProcessName;
        }
    }
}