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
			EnsureControlDisplaysCorrectly();

			var b = new Bitmap(control.Width, control.Height, PixelFormat.Format32bppArgb);
			control.DrawToBitmap(b, new Rectangle(0, 0, control.Width, control.Height));
			b.Save(received, ImageFormat.Png);

			return received;
		}


		private void EnsureControlDisplaysCorrectly()
		{
			AddToHiddenForm();
		}

		private void AddToHiddenForm()
		{
			var tempForm = new Form
			               	{
			               		ShowInTaskbar = false,
			               		AllowTransparency = true,
			               		Opacity = 0
			               	};

			tempForm.Controls.Add(control);
			tempForm.Show();
			control.Show();
		}
	}
}