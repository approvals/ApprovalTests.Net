using System;
using System.Windows;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class ClipboardReporter : IApprovalFailureReporter
	{
		public static readonly ClipboardReporter INSTANCE = new ClipboardReporter();
		public void Report(string approved, string received)
		{
			Clipboard.SetText(QuietReporter.GetCommandLineForApproval(approved, received));
		}
	}
}