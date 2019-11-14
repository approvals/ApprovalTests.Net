﻿using ApprovalTests.Core;
using ApprovalTests.Utilities;

namespace ApprovalTests.Reporters.ContinuousIntegration
{
    public class TfsReporter : IEnvironmentAwareReporter
    {
        public static readonly TfsReporter INSTANCE = new TfsReporter();

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return "TFSBuildServiceHost" == ParentProcessUtils.ParentProcessName;
        }

        public void Report(string approved, string received)
        {
            ContinuousDeliveryUtils.ReportOnServer(approved, received);
        }
    }
}