using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters.ContinuousIntegration
{
    public class TeamCityReporter : IEnvironmentAwareReporter
    {
        public static readonly TeamCityReporter INSTANCE = new TeamCityReporter();

        public void Report(string approved, string received)
        {
            ContinuousDeliveryUtils.ReportOnServer(approved, received);
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return Environment.GetEnvironmentVariable("TEAMCITY_PROJECT_NAME") != null;
        }
    }
}