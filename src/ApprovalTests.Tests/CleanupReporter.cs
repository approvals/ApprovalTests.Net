using ApprovalTests.Core;

public class CleanupReporter : IApprovalFailureReporter
{
    public void Report(string approved, string received) =>
        File.Delete(received);

    public bool ApprovedWhenReported() => false;
}