using System;
using System.Linq;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters.Windows
{
    public class RiderReporter : GenericDiffReporter
    {
        public static readonly RiderReporter INSTANCE;
        private static string PATH;

        static RiderReporter()
        {
            try
            {
                PATH = FindPath();
            }
            catch (Exception)
            {
                PATH = null;
            }
            
            INSTANCE = new RiderReporter();
        }

        public RiderReporter()
            : base(
                PATH ?? "Not launched from Rider.",
                "diff {0} {1}",
                "Couldn't find Rider at " + PATH)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            return OsUtils.IsWindowsOs() &&
                   base.IsWorkingInThisEnvironment(forFile) &&
                   PATH != null;
        }

        private static string FindPath()
        {
            var processAndParent = ParentProcessUtils.CurrentProcessWithAncestors().ToArray();
            using (var process = processAndParent.FirstOrDefault(x => x.MainModule.FileName.EndsWith("rider64.exe")))
            {
                if (process != null)
                {
                    var processModule = process.MainModule;
                    return processModule?.FileName;
                }
            }

            return null;
        }
    }
}