using System.Linq;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class MightyMooseAutoTestReporter: IEnvironmentAwareReporter
	{
		public static readonly MightyMooseAutoTestReporter INSTANCE = new MightyMooseAutoTestReporter();

		public void Report(string approved, string received)
		{
			// do nothing
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return Approvals.CurrentCaller.NonLambdaCallers
			                .Any(c => c.Class.Assembly.GetName().Name.StartsWith("AutoTest.TestRunners"));
		}
	}
}