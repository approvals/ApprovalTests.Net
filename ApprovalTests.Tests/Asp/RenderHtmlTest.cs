# if DEBUG
using System;
using System.Text.RegularExpressions;
using ApprovalTests.Asp;
using ApprovalTests.Reporters;
using Asp.Net.Demo.Orders;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter), typeof(FileLauncherReporter))]
	public class RenderHtmlTest
	{
		public const string AspViewState = "<input type=\"hidden\" name=\"__VIEWSTATE\".+/>";
		[Test]
		public void TestSimpleInvoice()
		{
			PortFactory.AspPort = 1359;
			Func<string, string> htmlScrubber = s => Regex.Replace(s, AspViewState, "<!-- aspviewstate -->");
			AspApprovals.VerifyAspPage(new InvoiceView().TestSimpleInvoice, htmlScrubber);

			//  -- These are the same thing
			//AspApprovals.VerifyUrl("http://localhost:1359/Orders/InvoiceView.aspx?TestSimpleInvoice");
		}
	}
}
#endif