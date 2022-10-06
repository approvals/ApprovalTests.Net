using TextCopy;

namespace ApprovalTests.Reporters;

public class ClipboardReporter : IApprovalFailureReporter
{
    public static readonly ClipboardReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        var text = QuietReporter.GetCommandLineForApproval(approved, received);
        ClipboardService.SetText(text);
    }
}