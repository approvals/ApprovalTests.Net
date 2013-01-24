using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class TeamCityReporter : IEnvironmentAwareReporter
	{
		public static readonly TeamCityReporter INSTANCE = new TeamCityReporter();

		public void Report(string approved, string received)
		{
			// does nothing
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return Environment.GetEnvironmentVariable("TEAMCITY_PROJECT_NAME") != null;
		}
	}
}