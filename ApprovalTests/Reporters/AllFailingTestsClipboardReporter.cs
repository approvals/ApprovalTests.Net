using System.Text;
using ApprovalTests.Core;
using TextCopy;

namespace ApprovalTests.Reporters
{
    public class AllFailingTestsClipboardReporter : IApprovalFailureReporter
    {
        private static StringBuilder TOTAL = new StringBuilder();
        public static readonly AllFailingTestsClipboardReporter INSTANCE = new AllFailingTestsClipboardReporter();

        public void Report(string approved, string received)
        {
            var temp = QuietReporter.GetCommandLineForApproval(approved, received);
            TOTAL.AppendLine(temp);
            Clipboard.SetText(TOTAL.ToString());
        }

        public bool ShouldIgnoreLineEndings { get; set; }
    }
}