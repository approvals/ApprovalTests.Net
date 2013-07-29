using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class CruiseControlNetReporter : IEnvironmentAwareReporter
	{
		public static readonly CruiseControlNetReporter INSTANCE = new CruiseControlNetReporter();

		public void Report(string approved, string received)
		{
			// does nothing
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
            return Environment.GetEnvironmentVariable("CCNetBuildDate") != null;
		}
	}
}