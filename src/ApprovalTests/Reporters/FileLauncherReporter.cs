using System.Diagnostics;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters;

public class FileLauncherReporter : IApprovalFailureReporter
{
    public static readonly FileLauncherReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        QuietReporter.DisplayCommandLineApproval(approved, received);
        Process.Start(received);
    }
}