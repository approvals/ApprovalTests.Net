#if !__MonoCS__
using System.Windows;
using ApprovalTests.Core;
using ApprovalUtilities.Wpf;

namespace ApprovalTests.Wpf
{
	internal class ApprovalWpfWindowWriter : IApprovalWriter
	{
		private readonly Window window;

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
#endif
