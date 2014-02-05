﻿using System;
using System.Windows;
using System.Windows.Controls;
using ApprovalTests.Core;
using ApprovalTests.Namers;
using ApprovalUtilities.Wpf;

namespace ApprovalTests.Wpf
{
	public class WpfApprovals
	{
		private static Action addAdditionalInfo = ApprovalResults.UniqueForOs;

		public static void RegisterDefaultAddtionalInfo(Action a)
		{
			addAdditionalInfo = a;

		}
		public static void Verify(Window window)
		{
			addAdditionalInfo();
			Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(window, f)));
		}

		

		public static void Verify(Func<Window> action)
		{
			Approvals.Verify(CreateWindowWpfWriter(action));
		}

		private static IApprovalWriter CreateWindowWpfWriter(Func<Window> action)
		{
			return new ImageWriter(f => WpfUtils.ScreeenCaptureInStaThread(f, action));
		}

		public static void Verify(Control control)
		{
			addAdditionalInfo();
			Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(control, f)));
		}

        public static void Verify(Control control, object viewModel)
        {
            // Maybe `Verify(new Window { Content = control, DataContext = viewModel })`
            throw new NotImplementedException();
        }
    }
}