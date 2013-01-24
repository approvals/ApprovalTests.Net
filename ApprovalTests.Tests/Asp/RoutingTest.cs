using ApprovalTests.Asp;
using ApprovalTests.Reporters;
using Asp.Net.Demo.Orders;
using MvcApplication1;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp
{
	[TestFixture]
	[UseReporter(typeof(TortoiseDiffReporter))]
	public class RoutingTest
	{
		[Test]
		public void TestRoutes()
		{
			var urls = new[] {"/Home/Index/Hello", "/"};
			AspApprovals.VerifyRouting(MvcApplication.RegisterRoutes, urls);
		}
	}
}
