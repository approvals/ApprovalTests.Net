﻿using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters.ContinuousIntegration
{
    public class JenkinsReporter : IEnvironmentAwareReporter
    {
        public static readonly JenkinsReporter INSTANCE = new JenkinsReporter();

        public void Report(string approved, string received)
        {
            ContinousDeliveryUtils.ReportOnServer(approved, received);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("JENKINS_URL") != null;
        }
    }
}