using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class ClipboardReporter : IApprovalFailureReporter
	{
		public static readonly ClipboardReporter INSTANCE = new ClipboardReporter();
		public void Report(string approved, string received)
		{
			string text = QuietReporter.GetCommandLineForApproval(approved, received);
			ClipboardUtilities.CopyToClipboard(text);
		}
	}
}