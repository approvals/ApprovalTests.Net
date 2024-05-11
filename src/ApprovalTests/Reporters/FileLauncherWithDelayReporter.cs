using ApprovalTests.Core;
using ApprovalTests.Reporters;

public class FileLauncherWithDelayReporter(int seconds = 2) :
    IApprovalFailureReporter
{
    public static readonly FileLauncherWithDelayReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
        FileLauncherReporter.INSTANCE.Report(approved, received);
        Thread.Sleep(seconds * 1000);
    }
}