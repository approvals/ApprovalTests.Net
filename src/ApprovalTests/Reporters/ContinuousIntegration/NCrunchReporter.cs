using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters.ContinuousIntegration;

public class NCrunchReporter : IEnvironmentAwareReporter
{
    public const string EnvironmentVariable = "NCrunch";
    public static readonly NCrunchReporter INSTANCE = new();

    public void Report(string approved, string received)
    {
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        var ncrunch = Environment.GetEnvironmentVariable(EnvironmentVariable);
        return ncrunch == "1";
    }
}