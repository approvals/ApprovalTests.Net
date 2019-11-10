using System.Text;
using ApprovalTests.Core;
using TextCopy;

namespace ApprovalTests.Reporters
{
    public class AllFailingTestsClipboardReporter : IApprovalFailureReporter
    {
        static StringBuilder builder = new StringBuilder();

        public void Report(string approved, string received)
        {
            var temp = QuietReporter.GetCommandLineForApproval(approved, received);
            lock (builder)
            {
                builder.AppendLine(temp);
                Clipboard.SetText(builder.ToString());
            }
        }
    }
}