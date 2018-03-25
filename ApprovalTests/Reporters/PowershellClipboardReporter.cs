#if !NETCORE
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class PowerShellClipboardReporter : IApprovalFailureReporter
	{
		public static readonly PowerShellClipboardReporter INSTANCE = new PowerShellClipboardReporter();
		public void Report(string approved, string received)
		{
			string text = GetCommandLineForApproval(approved, received);
			ClipboardUtilities.CopyToClipboard(text);
		}

		public static  string GetCommandLineForApproval(string approved, string received)
		{
			return $"Move-Item \"{received}\" \"{approved}\" -Force";
		}
	}
}
#endif