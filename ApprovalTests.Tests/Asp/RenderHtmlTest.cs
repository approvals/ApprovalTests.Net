# if DEBUG
using ApprovalTests.Scrubber;
using System;
using System.Text.RegularExpressions;
using ApprovalTests.Asp;
using ApprovalTests.Reporters;
using Asp.Net.Demo;
using Asp.Net.Demo.Orders;
using CassiniDev;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp
{
	[TestFixture]
	[UseReporter(typeof (DiffReporter), typeof (FileLauncherReporter))]
	public class RenderHtmlTest
	{
		private CassiniDevServer server = new CassiniDevServer();

		[TestFixtureSetUp]
		public void Setup()
		{
			PortFactory.AspPort = 1359;
			server.StartServer(Global.Path, PortFactory.AspPort, "/", "localhost");
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			server.StopServer();
		}

		[Test]
		public void TestSimpleInvoice()
		{
			AspApprovals.VerifyAspPage(new InvoiceView().TestSimpleInvoice, HtmlScrubbers.ScrubAsp);

			//  -- These are the same thing
			//AspApprovals.VerifyUrl("http://localhost:1359/Orders/InvoiceView.aspx?TestSimpleInvoice");
		}

		[Test]
		public void TestInternationalization()
		{
			AspApprovals.VerifyUrl("http://localhost:1359/Encoding.UTF8.html", HtmlScrubbers.ScrubBrowserLink);
		}
	}
}

#endif