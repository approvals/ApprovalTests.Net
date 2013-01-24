using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ImageTools;
using ApprovalTests.Silverlight.ServiceReference;

namespace ApprovalTests.Silverlight
{
	public static class Approvals
	{
		public static void Approve<T>(string path, string testName, IEnumerable<T> enumerable, string label)
		{
			var client = new ApprovalServiceClient();
			string result = EnumerableWriter.Write(enumerable, label);
			client.ApproveAsync(path, testName, Encoding.UTF8.GetBytes(result));
		}

		public static void Approve<T>(string path, string testName, IEnumerable<T> enumerable, string label, EnumerableWriter.CustomFormatter<T> formatter)
		{
			var client = new ApprovalServiceClient();
			string result = EnumerableWriter.Write(enumerable, label, formatter);
			client.ApproveAsync(path, testName, Encoding.UTF8.GetBytes(result));
		}

		public static void Approve<T>(string path, string testName, IEnumerable<T> enumerable, EnumerableWriter.CustomFormatter<T> formatter)
		{
			var client = new ApprovalServiceClient();
			string result = EnumerableWriter.Write(enumerable, formatter);
			client.ApproveAsync(path, testName, Encoding.UTF8.GetBytes(result));
		}


		public static void Approve(string path, string testName, Control control)
		{
			var client = new ApprovalServiceClient();
			client.ApproveAsync(path, testName, CaptureControl(control));
		}

		public static byte[] CaptureControl(Control control)
		{
			var bitmap = new WriteableBitmap((int)control.ActualWidth, (int)control.ActualHeight);
			bitmap.Render(control, null);
			bitmap.Invalidate();

			byte[] bytes;
			using (var stream = new CustomMemoryStream())
			{
				ImageTools.Image image = bitmap.ToImage();
				image.WriteToStream(stream);
				stream.Position = 0;
				bytes = new byte[stream.Length];
				stream.Read(bytes, 0, bytes.Length);
				stream.ActualClose();
			}

			return bytes;
		}
	}

	public class CustomMemoryStream : MemoryStream
	{
		public override void Close()
		{
		}

		public void ActualClose()
		{
			base.Close();
		}
	}
}