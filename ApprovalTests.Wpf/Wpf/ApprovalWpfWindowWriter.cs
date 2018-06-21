using System.Windows;
using ApprovalTests.Core;
using ApprovalUtilities.Wpf;

namespace ApprovalTests.Wpf
{
    public class ApprovalWpfWindowWriter : IApprovalWriter
    {
        Window window;

        public ApprovalWpfWindowWriter(Window window)
        {
            this.window = window;
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
            WpfUtils.ScreenCapture(window, received);
            return received;
        }
    }
}