using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MachineSpecific.Tests.Namer
{
	[TestClass]
	public class VisualStudioReporterTest
	{
		[TestMethod]
		public void WhenLaunchedFromVisualStudioThenIsWorkingInThisEnvironmentForTextFiles()
		{
			var visualStudioReporter = new VisualStudioReporter();
			Assert.IsTrue(visualStudioReporter.IsWorkingInThisEnvironment("someFile.txt"));
		}
	}
}
