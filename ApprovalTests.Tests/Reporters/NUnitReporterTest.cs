using System.IO;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class NUnitReporterTest
	{
		[Test]
		public void TestNunitIsWorking()
		{
			Approvals.SetCaller();
			Assert.IsTrue(NUnitReporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
		}

		[Test]
		[UseReporter((typeof (NUnitReporterWithCleanup)))]
		public void TestReporter()
		{
			try
			{
				Approvals.Verify("Hello");
			}
			catch (AssertionException e)
			{
				var expectedMessage = string.Format ("  String lengths are both 5. Strings differ at index 0.{0}  Expected: \"World\"{0}  But was:  \"Hello\"{0}  -----------^{0}", System.Environment.NewLine);
				Assert.AreEqual(
					expectedMessage,
					e.Message);
			}
		}
	}

	public class NUnitReporterWithCleanup : NUnitReporter
	{
		public override void Report(string approved, string received)
		{
			try
			{
				base.Report(approved, received);
			}
			finally
			{
				File.Delete(received);
			}
		}
	}
}