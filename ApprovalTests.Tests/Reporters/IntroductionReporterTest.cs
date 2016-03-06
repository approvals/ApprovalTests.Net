using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class IntroductionReporterTest
	{
		[Test]
		public void TestComment()
		{
			Approvals.Verify(new IntroductionReporter().GetFriendlyWelcomeMessage());
		}
	}
}