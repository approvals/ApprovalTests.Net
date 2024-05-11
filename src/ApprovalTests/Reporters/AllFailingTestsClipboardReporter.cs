namespace ApprovalTests.Reporters;

public class AllFailingTestsClipboardReporter : IApprovalFailureReporter
{
    static StringBuilder builder = new();
    public static readonly AllFailingTestsClipboardReporter INSTANCE = new();

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