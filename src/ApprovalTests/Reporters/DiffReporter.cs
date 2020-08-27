﻿using System;
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
            var launch = DiffRunner.Launch(received, approved)
                .GetAwaiter()
                .GetResult();
            if (launch == LaunchResult.NoDiffToolFound)
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