using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalDemos.Data
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class HtmlTest
    {
        [Test]
        public void TestLambdas()
        {
            string html = "<html><body><h1>Hello World</h1><table><tr><td>1 &nbsp;</td><td colspan=2>5</td></tr><tr><td colspan=3>Hello</td></tr></table></body></html>".Replace(">", ">\r\n");
            Approvals.VerifyHtml(html);
        }

        [Test]
        [UseReporter(typeof(VisualStudioReporter))]
        public void TestVsReporter()
        {
            Approvals.SetCaller();
            Assert.True(VisualStudioReporter.INSTANCE.IsWorkingInThisEnvironment("a.txt"));
            Approvals.Verify("Hello");
        }
    }
}