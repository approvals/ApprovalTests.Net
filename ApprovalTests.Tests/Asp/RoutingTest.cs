using ApprovalTests.Asp;
using ApprovalTests.Reporters;
using MvcApplication1;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp
{
	[TestFixture]
	[UseReporter(typeof (ClipboardReporter))]
	public class RoutingTest
	{
		[Test]
		public void TestRoutes()
		{
			var urls = new[] {"/Home/Index/Hello", "/"};
			AspApprovals.VerifyRouting(MvcApplication.RegisterRoutes, urls);
		}

	    [Test]
	    public void TestMissingRoutes(){
	    AspApprovals.VerifyRouting(r => { }, "/");
	    }
	}
}