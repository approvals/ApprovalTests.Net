using System;
using System.Diagnostics;
using System.Linq;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
    public class RiderReporter : GenericDiffReporter
    {
        public static readonly RiderReporter INSTANCE = new RiderReporter();
        private static string PATH;

        public RiderReporter()
            : base(
                GetPath(),
                "diff {0} {1}",
                "Couldn't find Rider at " + PATH)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            return OsUtils.IsWindowsOs() && base.IsWorkingInThisEnvironment(forFile) && LaunchedFromRider();
        }

        private static string GetPath()
        {
            LaunchedFromRider();
            return PATH ?? "Not launched from Rider.";
        }

        private static bool LaunchedFromRider()
        {
            if (PATH != null)
            {
                return true;
            }

            var processAndParent = ParentProcessUtils.CurrentProcessWithAncestors().ToArray();

            Process process;

            try
            {
                process = processAndParent.FirstOrDefault(x => x.MainModule.FileName.EndsWith("rider64.exe"));
            }
            catch (Exception)
            {
                // Any exception means we are not working in this environment.
                return false;
            }

            if (process != null)
            {
                var processModule = process.MainModule;
                PATH = processModule?.FileName;
            }

            return PATH != null;
        }
    }
}