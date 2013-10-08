using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ApprovalTests.WinForms
{
    public class WinFormsUtils
    {
        public static void ScreenCapture(string received, Form test)
        {
            using (var hidden = new Form())
            {
                EnsureFormDisplaysCorrectlyByAddingItToAHiddenForm(hidden, test);
                SavePng(received, test);
            }
        }

        private static void SavePng(string received, Form test)
        {
            using (var b = new Bitmap(test.Width, test.Height, PixelFormat.Format32bppArgb))
            {
                test.DrawToBitmap(b, new Rectangle(0, 0, test.Width, test.Height));
                b.Save(received, ImageFormat.Png);
            }
        }

        private static void EnsureFormDisplaysCorrectlyByAddingItToAHiddenForm(Form hidden, Form test)
        {
            hidden.IsMdiContainer = true;
            hidden.ShowInTaskbar = false;
            hidden.AllowTransparency = true;
            hidden.Opacity = 0;

            test.MdiParent = hidden;
            hidden.Show();
            test.Show();
        }
    }
}