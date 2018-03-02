using System.Windows.Forms;
using ApprovalTests.Core;
using ApprovalTests.Writers;

namespace ApprovalTests.WinForms
{
    public class ApprovalControlWriter : IApprovalWriter
    {
        private readonly Control control;

        public ApprovalControlWriter(Control controlHandle)
        {
            control = controlHandle;
        }

        public string GetApprovalFilename(string basename)
        {
            return basename + WriterUtils.Approved + ".png";
        }

        public string GetReceivedFilename(string basename)
        {
            return basename + WriterUtils.Received + ".png";
        }

        public string WriteReceivedFile(string received)
        {
            return WinFormsUtils.ScreenCapture(received, control);
        }
    }
}