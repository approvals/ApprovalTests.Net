using System.Text;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class AllFailingTestsClipboardReporter : IApprovalFailureReporter
	{
		private static StringBuilder TOTAL = new StringBuilder();
		public static readonly AllFailingTestsClipboardReporter INSTANCE = new AllFailingTestsClipboardReporter();

		public void Report(string approved, string received)
		{
			string temp = QuietReporter.GetCommandLineForApproval(approved, received);
			TOTAL.AppendLine(temp);
			ClipboardUtilities.CopyToClipboard(TOTAL.ToString());
		}
	}
}