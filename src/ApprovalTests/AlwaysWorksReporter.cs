class AlwaysWorksReporter(IApprovalFailureReporter reporter) :
    IEnvironmentAwareReporter
{
    public void Report(string approved, string received) =>
        reporter.Report(approved, received);

    public bool IsWorkingInThisEnvironment(string forFile) => true;
}