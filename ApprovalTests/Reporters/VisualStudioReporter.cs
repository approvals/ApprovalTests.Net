using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ApprovalTests.Reporters
{
    using System;
    using System.ComponentModel;

    public class VisualStudioReporter : GenericDiffReporter
    {
        public static readonly VisualStudioReporter INSTANCE = new VisualStudioReporter();
        private static string PATH;

        public VisualStudioReporter()
            : base(
                    GetPath(),
                    "/diff \"{0}\" \"{1}\"",
                    "Couldn't find Visual Studio at " + PATH)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            return base.IsWorkingInThisEnvironment(forFile) && LaunchedFromVisualStudio();
        }

        private static Process GetParentProcess(Process currentProcess)
        {
            try
            {
                var pc = new PerformanceCounter("Process", "Creating Process Id", currentProcess.ProcessName);
                return Process.GetProcessById((int)pc.RawValue);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static string GetPath()
        {
            LaunchedFromVisualStudio();
            return PATH ?? "Not launched from Visual Studio.";
        }

        private static IEnumerable<Process> GetProcessAndParent()
        {
            var currentProcess = Process.GetCurrentProcess();
            yield return currentProcess;
            var parentProcess = GetParentProcess(currentProcess);
            do
            {
                yield return parentProcess;
                parentProcess = GetParentProcess(parentProcess);
            }
            while (parentProcess != null);
        }

        private static bool LaunchedFromVisualStudio()
        {
            if (PATH != null)
            {
                return true;
            }

            var processAndParent = GetProcessAndParent().ToArray();

            Process process = null;

            try
            {
                process = processAndParent.FirstOrDefault(x => x.MainModule.FileName.EndsWith("devenv.exe"));
            }
            catch (Win32Exception ex)
            {
                if (ex.Message == "A 32 bit processes cannot access modules of a 64 bit process.")
                {
                    // devenv is always 32 bit; therefore not parent.
                    return false;
                }

                throw;
            }

            if (process != null)
            {
                var processModule = process.MainModule;
                var version = processModule.FileVersionInfo.FileMajorPart;
                if (11 <= version)
                {
                    PATH = processModule.FileName;
                }
            }

            return PATH != null;
        }
    }
}