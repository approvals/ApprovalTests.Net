using System.Drawing;
using System.Windows.Forms;
using ApprovalTests.Reporters;
using ApprovalTests.Tests.WinForms;
using ApprovalTests.WinForms;
using NUnit.Framework;
using Button = System.Windows.Forms.Button;

namespace ApprovalTests.MachineSpecific.Tests.WinForms
{
    [TestFixture]
    [UseReporter(typeof(AllFailingTestsClipboardReporter), typeof(ImageReporter))]
    public class WinFormTests
    {
        [Test]
        public void TestControlApproved()
        {
            WinFormsApprovals.Verify(new Button
            {
                BackColor = Color.LightBlue,
                Text = "Help"
            });
        }

        [Test]
        public void TestFormApproval()
        {
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