using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class NCrunchReporter : IEnvironmentAwareReporter
	{
	    public const string EnviromentVariable = "NCrunch";
	    public static readonly NCrunchReporter INSTANCE = new NCrunchReporter();

		public void Report(string approved, string received)
		{
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
		    var ncrunch = Environment.GetEnvironmentVariable(EnviromentVariable);
		    return ncrunch == "1";

		}
	}
}