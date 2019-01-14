using ApprovalTests.Core;
using TextCopy;

namespace ApprovalTests.Reporters
{
    public class PowerShellClipboardReporter : IApprovalFailureReporter
    {
        public static readonly PowerShellClipboardReporter INSTANCE = new PowerShellClipboardReporter();

        public void Report(string approved, string received)
        {
            var text = GetCommandLineForApproval(approved, received);
            Clipboard.SetText(text);
        }

        public bool ShouldIgnoreLineEndings { get; set; }

        public static string GetCommandLineForApproval(string approved, string received)
        {
            return $"Move-Item \"{received}\" \"{approved}\" -Force";
        }
    }
}