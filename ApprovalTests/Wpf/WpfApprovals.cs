#if !__MonoCS__
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
		private static Func<IDisposable> addAdditionalInfo = ApprovalResults.UniqueForOs;

		public static void RegisterDefaultAddtionalInfo(Func<IDisposable> a)
		{
			addAdditionalInfo = a;
		}

		public static void Verify(Window window)
		{
			using (addAdditionalInfo())
			{
				Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(window, f)));
			}
		}


		public static void Verify(Func<Window> windowCreator)
		{
			Approvals.Verify(CreateWindowWpfWriter(windowCreator));
		}

		private static IApprovalWriter CreateWindowWpfWriter(Func<Window> windowCreator)
		{
			return new ImageWriter(f => WpfUtils.ScreenCaptureInStaThread(f, windowCreator));
		}

        public static void Verify(Func<Control> action)
        {
            ApprovalTests.Approvals.Verify(CreateControlWpfWriter(action));
        }

        private static IApprovalWriter CreateControlWpfWriter(Func<Control> action)
        {
            return new ImageWriter(f => WpfUtils.ScreenCaptureInStaThread(f, action));
        }

		public static void Verify(Control control)
		{
			using (addAdditionalInfo())
			{
				Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(control, f)));
			}
		}
	}
}
#endif