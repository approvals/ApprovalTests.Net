using ApprovalTests.Core;
using TextCopy;

namespace ApprovalTests.Reporters;

public class PowerShellClipboardReporter : IApprovalFailureReporter
{
    public static readonly PowerShellClipboardReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        var text = GetCommandLineForApproval(approved, received);
        ClipboardService.SetText(text);
    }

    public static string GetCommandLineForApproval(string approved, string received)
    {
        return $"Move-Item \"{received}\" \"{approved}\" -Force";
    }
}