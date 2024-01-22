using ApprovalTests.Core;

namespace ApprovalTests.Reporters;

public class InvalidReporterConfiguration(Type reporter) :
    IEnvironmentAwareReporter
{
    public void Report(string approved, string received) =>
        throw BuildException();

    public bool IsWorkingInThisEnvironment(string forFile) =>
        throw BuildException();

    Exception BuildException()
    {
        throw new($"""
                   Invalid configuration of reporter. Reporters must extend {nameof(IApprovalFailureReporter)}.
                   Invalid reporter type: {reporter.FullName}

                   Note: The stack here is not helpful.
                   """);
    }
}