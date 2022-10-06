namespace ApprovalTests.Reporters.TestFrameworks;

public class NUnitReporter : AssertReporter
{
    public readonly static NUnitReporter INSTANCE = new();

    public NUnitReporter()
        : base("NUnit.Framework.Assert, nunit.framework", "AreEqual", NUnitStackTraceParser.Attribute)
    {
    }
}