using System;
using System.IO;
using ApprovalTests.Core;
using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class DiffReporter : IEnvironmentAwareReporter
    {
        public static readonly DiffReporter INSTANCE = new DiffReporter();

        public void Report(string approved, string received)
        {
            if (DiffRunner.Launch(received, approved) == LaunchResult.NoDiffToolForExtension)
            {
                throw new Exception($"Could not find a diff tool for extension: {Path.GetExtension(received)}");
            }
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return true;
        }
    }
}