using System.Windows.Forms;
using ApprovalTests.Core;

namespace ApprovalTests.WinForms
{
    public class ApprovalFormWriter : IApprovalWriter
    {
        private readonly Form form;

        public ApprovalFormWriter(Form form)
        {
            this.form = form;
        }

        public string GetApprovalFilename(string basename)
        {
            return basename + ".approved.png";
        }

        public string GetReceivedFilename(string basename)
        {
            return basename + ".received.png";
        }

        public string WriteReceivedFile(string received)
        {
            return WinFormsUtils.ScreenCapture(received, form);
        }
    }
}