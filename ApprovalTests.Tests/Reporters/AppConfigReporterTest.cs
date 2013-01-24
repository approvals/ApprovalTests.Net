using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class AppConfigReporterTest
	{
		[Test]
		public void Test()
		{
			Assert.IsInstanceOf<DiffReporter>(new AppConfigReporter().Reporter);
		}
	}
}