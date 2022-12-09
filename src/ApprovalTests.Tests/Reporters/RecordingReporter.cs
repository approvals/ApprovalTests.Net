using ApprovalTests.Core;

public class RecordingReporter : IEnvironmentAwareReporter
{
    readonly bool working;

    public RecordingReporter()
    {
        working = true;
    }

    public RecordingReporter(bool working)
    {
        this.working = working;
    }

    public void Report(string approved, string received)
    {
        CalledWith = $"{approved},{received}";
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        return working;
    }

    public string CalledWith { get; set; }
}