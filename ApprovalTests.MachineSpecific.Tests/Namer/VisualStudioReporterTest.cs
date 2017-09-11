using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.MachineSpecific.Tests.Namer
{
	[TestFixture]
	public class VisualStudioReporterTest
	{
		[Test]
		public void WhenLaunchedFromVisualStudioThenIsWorkingInThisEnvironmentForTextFiles()
		{
			var visualStudioReporter = new VisualStudioReporter();
			Assert.IsTrue(visualStudioReporter.IsWorkingInThisEnvironment("someFile.txt"));
		}
	}
}
