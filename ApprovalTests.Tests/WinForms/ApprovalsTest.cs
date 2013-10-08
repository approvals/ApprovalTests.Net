using System.Drawing;
using System.Windows.Forms;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ApprovalTests.WinForms;
using NUnit.Framework;

namespace ApprovalTests.Tests.WinForms
{
    [TestFixture]
    [UseReporter(typeof(AllFailingTestsClipboardReporter), typeof(FileLauncherReporter))]
    public class ApprovalsTest
    {
        [Test]
        public void TestControlApproved()
        {
            ApprovalResults.UniqueForMachineName();
            WinFormsApprovals.Verify(new Button { BackColor = Color.LightBlue, Text = "Help" });
        }

        [Test]
        public void TestFormApproval()
        {
            ApprovalResults.UniqueForMachineName();
            WinFormsApprovals.Verify(new Form());
        }

        [Test]
        [UseReporter(typeof(TortoiseDiffReporter))]
        public void VerifyCompleteFormTest()
        {
            WinFormsApprovals.VerifyEventsFor(new DemoForm());
        }
    }
}