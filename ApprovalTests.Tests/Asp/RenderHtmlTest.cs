# if DEBUG

using System;
using System.Text.RegularExpressions;
using ApprovalTests.Asp;
using ApprovalTests.Reporters;
using Asp.Net.Demo;
using Asp.Net.Demo.Orders;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter), typeof(FileLauncherWithDelayReporter))]
    public class RenderHtmlTest : ServerDependentTest
    {
        public const string AspViewState = "<input type=\"hidden\" name=\"__VIEWSTATE\".+/>";

        public RenderHtmlTest()
            : base(Global.Directory, 1359)
        {
        }

        [Test]
        public void TestSimpleInvoice()
        {
            Func<string, string> htmlScrubber = s => Regex.Replace(s, AspViewState, "<!-- aspviewstate -->");
            AspApprovals.VerifyAspPage(new InvoiceView().TestSimpleInvoice, htmlScrubber);

            //  -- These are the same thing
            //AspApprovals.VerifyUrl("http://localhost:1359/Orders/InvoiceView.aspx?TestSimpleInvoice");
        }
    }
}

#endif