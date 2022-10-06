namespace ApprovalTests.Reporters;

public class InvalidReporterConfiguration : IEnvironmentAwareReporter
{
    readonly Type reporter;

    public InvalidReporterConfiguration(Type reporter)
    {
        this.reporter = reporter;
    }

    public void Report(string approved, string received)
    {
        throw BuildException();
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        throw BuildException();
    }

    Exception BuildException()
    {
        throw new Exception($@"Invalid configuration of reporter. Reporters must extend {nameof(IApprovalFailureReporter)}.
Invalid reporter type: {reporter.FullName}

Note: The stack here is not helpful.");
    }
}