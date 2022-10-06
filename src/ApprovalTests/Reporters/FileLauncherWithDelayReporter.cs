using ApprovalTests.Reporters;

public class FileLauncherWithDelayReporter : IApprovalFailureReporter
{
    public static readonly FileLauncherWithDelayReporter INSTANCE = new();

    readonly int seconds;

    public FileLauncherWithDelayReporter(int seconds = 2)
    {
        this.seconds = seconds;
    }

    public void Report(string approved, string received)
    {
        FileLauncherReporter.INSTANCE.Report(approved, received);
        Thread.Sleep(seconds * 1000);
    }
}