using System.Text;
using ApprovalTests.Core;
using TextCopy;

namespace ApprovalTests.Reporters;

public class AllFailingTestsClipboardReporter : IApprovalFailureReporter
{
    static StringBuilder builder = new StringBuilder();
    public static readonly AllFailingTestsClipboardReporter INSTANCE = new AllFailingTestsClipboardReporter();

    public void Report(string approved, string received)
    {
        var temp = QuietReporter.GetCommandLineForApproval(approved, received);
        lock (builder)
        {
            builder.AppendLine(temp);
            ClipboardService.SetText(builder.ToString());
        }
    }
}