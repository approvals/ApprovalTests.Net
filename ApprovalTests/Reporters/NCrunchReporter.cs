using System.Linq;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class NCrunchReporter : IEnvironmentAwareReporter
	{
		public static readonly NCrunchReporter INSTANCE = new NCrunchReporter();

		public void Report(string approved, string received)
		{
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return Approvals.CurrentCaller.NonLambdaCallers
				.Any(c => c.Class.Assembly.GetName().Name.StartsWith("nCrunch.TestExecution"));
			;
		}
	}
}