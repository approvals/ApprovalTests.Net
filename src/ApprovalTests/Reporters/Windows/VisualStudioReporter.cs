﻿using System;
using System.Diagnostics;
using System.Linq;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters.Windows
{
    public class VisualStudioReporter : GenericDiffReporter
    {
        public static readonly VisualStudioReporter INSTANCE = new VisualStudioReporter();
        private static string PATH;

        public VisualStudioReporter()
            : base(
                GetPath(),
                "/diff {0} {1}",
                "Couldn't find Visual Studio at " + PATH)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            return OsUtils.IsWindowsOs() && base.IsWorkingInThisEnvironment(forFile) && LaunchedFromVisualStudio();
        }

        private static string GetPath()
        {
            LaunchedFromVisualStudio();
            return PATH ?? "Not launched from Visual Studio.";
        }


        private static bool LaunchedFromVisualStudio()
        {
            if (PATH != null)
            {
                return true;
            }

            var processAndParent = ParentProcessUtils.CurrentProcessWithAncestors().ToArray();

            Process process;

            try
            {
                process = processAndParent.FirstOrDefault(x => x.MainModule.FileName.EndsWith("devenv.exe"));
            }
            catch (Exception)
            {
                // Any exception means we are not working in this environment.
                return false;
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