using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace ApprovalTests.Tests.Reporters
{
	public class NonNUnitReporterTest
	{
		[Fact]
		public void TestNunitIsNotWorkingFromXUnit()
		{
			Approvals.SetCaller();
			Assert.IsFalse(NUnitReporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));

		}
		[Fact]
		public void TestXunitIsWorking()
		{
			Approvals.SetCaller();
			Assert.IsTrue(XUnitReporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
		}
		[Fact]
		public void TestXunitReporterIsWorking()
		{
			var e = ExceptionUtilities.GetException(() =>  XUnitReporter.INSTANCE.AssertEqual("Hello", "Hello2"));
			Approvals.Verify(e.Message);
		}
	}
}