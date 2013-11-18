using ApprovalTests.Asp;
using NUnit.Framework;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Tests.Asp
{
	[TestFixture]
	public class UrlTest
	{
		[Test]
		public void TestUrlResolving()
		{
			PortFactory.AspPort = 1000;
			var urls = new[]{"~/home","/home","http://www.google.com"};
			Approvals.VerifyAll("Resolving:", urls,
				u => "{0} => {1}".FormatWith(u,AspApprovals.ResolveUrl(u)));
		}
	}
}