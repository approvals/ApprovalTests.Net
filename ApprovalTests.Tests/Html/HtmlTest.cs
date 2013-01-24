using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Html
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter), typeof(FileLauncherReporter))]
	public class HtmlTest
	{
		[Test]
		public static void TestHtml()
		{
			Approvals.VerifyHtml("<html><body><div style='font-family:Broadway;font-size:18'> Web Page from ApprovalTests</div></body></html>");
		}
	}
}