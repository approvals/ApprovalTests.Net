using System.Drawing;
using System.Drawing.Imaging;
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
            using (Form hidden = new Form())
            {
                EnsureControlDisplaysCorrectly(hidden);
                SavePng(received);
                return received;
            }
        }

        private void SavePng(string received)
        {
            using (var b = new Bitmap(control.Width, control.Height, PixelFormat.Format32bppArgb))
            {
                control.DrawToBitmap(b, new Rectangle(0, 0, control.Width, control.Height));
                b.Save(received, ImageFormat.Png);
            }
        }

        private void EnsureControlDisplaysCorrectly(Form hidden)
        {
            AddToHiddenForm(hidden);
        }

        private void AddToHiddenForm(Form hidden)
        {
            hidden.ShowInTaskbar = false;
            hidden.AllowTransparency = true;
            hidden.Opacity = 0;

            hidden.Controls.Add(control);
            hidden.Show();
            control.Show();
        }
    }
}