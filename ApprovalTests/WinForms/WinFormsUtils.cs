using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ApprovalTests.WinForms
{
	public class WinFormsUtils
	{
		public static void ScreenCapture(string received, Form form)
		{
			EnsureControlDisplaysCorrectlyByAddingItToAHiddenForm(form);

			var b = new Bitmap(form.Width, form.Height, PixelFormat.Format32bppArgb);
			form.DrawToBitmap(b, new Rectangle(0, 0, form.Width, form.Height));
			b.Save(received, ImageFormat.Png);
		}

		private static void EnsureControlDisplaysCorrectlyByAddingItToAHiddenForm(Form form)
		{
			var tempForm = new Form
			               	{
			               		IsMdiContainer = true,
			               		ShowInTaskbar = false,
			               		AllowTransparency = true,
			               		Opacity = 0
			               	};

			form.MdiParent = tempForm;
			tempForm.Show();
			form.Show();
		}
	}
}