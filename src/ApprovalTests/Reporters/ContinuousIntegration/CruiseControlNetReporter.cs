using System;
using System.Diagnostics;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters.ContinuousIntegration
{
    public class CruiseControlNetReporter : IEnvironmentAwareReporter
    {
        public static readonly CruiseControlNetReporter INSTANCE = new CruiseControlNetReporter();

        public void Report(string approved, string received)
        {
            ContinuousDeliveryUtils.ReportOnServer(approved, received);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            var isWorking = Environment.GetEnvironmentVariable("CCNetBuildDate") != null;
            if (isWorking)
            {
                var message = "CruiseControl support is being deprecated. It will be removed in V5.";
                Trace.WriteLine(message);
                Console.WriteLine(message);
                Debug.WriteLine(message);
            }
            return isWorking;
        }
    }
}