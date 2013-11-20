using System.Diagnostics;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class NotepadLauncher : IApprovalFailureReporter
	{
		public static readonly NotepadLauncher INSTANCE = new NotepadLauncher();
		public void Report(string approved, string received)
		{
			QuietReporter.DisplayCommandLineApproval(approved, received);
			Process.Start(received);
		}
	}
}