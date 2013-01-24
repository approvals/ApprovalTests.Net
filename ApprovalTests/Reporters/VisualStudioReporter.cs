using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ApprovalTests.Reporters
{
    public class VisualStudioReporter : GenericDiffReporter
    {
        private static readonly string PATH = DotNet4Utilities.GetPathInProgramFilesX86(@"Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe");
        public static readonly VisualStudioReporter INSTANCE = new VisualStudioReporter();

        public VisualStudioReporter()
            : base(
                    PATH,
                    "/diff \"{0}\" \"{1}\"",
                    "Only works with VS11\r\nCouldn't find VS11 at " + PATH)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            return base.IsWorkingInThisEnvironment(forFile) && LaunchedFromVisualStudio();
        }

        private bool LaunchedFromVisualStudio()
        {
            return GetProcessAndParent().Any(x => x.MainModule.FileName == PATH);
        }

        private IEnumerable<Process> GetProcessAndParent()
        {
            var currentProcess = Process.GetCurrentProcess();
            var pc = new PerformanceCounter("Process", "Creating Process Id", currentProcess.ProcessName);
            var parentProcess = Process.GetProcessById((int)pc.RawValue);
            return new[] { currentProcess, parentProcess };
        }
    }
}