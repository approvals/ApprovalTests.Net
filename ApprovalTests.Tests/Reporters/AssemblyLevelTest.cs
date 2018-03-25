using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class AssemblyLevelTest
	{
		[Test]
		public void TestFixtureLevel()
		{
			Assert.AreEqual(typeof(DiffReporter), Approvals.GetReporter().GetType());
		}
	}
}