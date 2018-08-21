using ApprovalTests.Core;
using ApprovalUtilities.Utilities;
using TextCopy;

namespace ApprovalTests.Reporters
{
    public class ClipboardReporter : IApprovalFailureReporter
    {
        public static readonly ClipboardReporter INSTANCE = new ClipboardReporter();

        public void Report(string approved, string received)
        {
            var text = QuietReporter.GetCommandLineForApproval(approved, received);
            Clipboard.SetText(text);
        }
    }
}