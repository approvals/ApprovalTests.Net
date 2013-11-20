using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Win32;

namespace ApprovalTests.Reporters
{
    public class VisualStudioReporter : GenericDiffReporter
    {
        private static readonly string DEVENV_REGISTRYKEY =
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\devenv.exe";
        private static readonly string PATH = (string)Registry.GetValue(DEVENV_REGISTRYKEY, "", @"Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe"); 
        public static readonly VisualStudioReporter INSTANCE = new VisualStudioReporter();

        public VisualStudioReporter()
            : base(
                    PATH,
                    "/diff \"{0}\" \"{1}\"",
                    "Couldn't find Visual Studio at " + PATH)
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