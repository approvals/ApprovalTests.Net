using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class IntroductionReporterTest
	{
		[Test]
		[UseReporter(typeof(ClipboardReporter))]
		public void TestComment()
		{
			Approvals.Verify(new IntroductionReporter().GetFriendlyWelcomeMessage());
		}
	}
}