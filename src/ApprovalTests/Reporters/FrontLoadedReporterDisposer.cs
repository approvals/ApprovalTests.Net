using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters;

public class FrontLoadedReporterDisposer : IDisposable
{
    public static IEnvironmentAwareReporter Default = DefaultFrontLoaderReporter.INSTANCE;
    private readonly IEnvironmentAwareReporter previous;

    public FrontLoadedReporterDisposer(IEnvironmentAwareReporter reporter)
    {
        previous = Default;
        Default = reporter;
    }

    public void Dispose()
    {
        Default = previous;
    }
}