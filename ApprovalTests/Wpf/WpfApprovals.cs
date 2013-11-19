using System;
using System.Windows;
using System.Windows.Controls;
using ApprovalTests.Core;
using ApprovalTests.Namers;
using ApprovalUtilities.Wpf;

namespace ApprovalTests.Wpf
{
	public class WpfApprovals
	{
		public static void Verify(Window window)
		{
            //ApprovalResults.UniqueForOs();
			ApprovalTests.Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(window, f)));
		}

		public static void Verify(Func<Window> action)
		{
			ApprovalTests.Approvals.Verify(CreateWindowWpfWriter(action));
		}

		private static IApprovalWriter CreateWindowWpfWriter(Func<Window> action)
		{
			return new ImageWriter(f => WpfUtils.ScreeenCaptureInStaThread(f, action));
		}

        public static void Verify(Func<Control> action)
        {
            ApprovalTests.Approvals.Verify(CreateWindowWpfWriter(action));
        }

        private static IApprovalWriter CreateWindowWpfWriter(Func<Control> action)
        {
            return new ImageWriter(f => WpfUtils.ScreeenCaptureInStaThread(f, action));
        }

		public static void Verify(Control control)
		{
			//ApprovalResults.UniqueForOs();
			ApprovalTests.Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(control, f)));
		}
	}
}