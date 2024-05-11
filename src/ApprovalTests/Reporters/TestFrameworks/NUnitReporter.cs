namespace ApprovalTests.Reporters.TestFrameworks;

public class NUnitReporter() :
    AssertReporter("NUnit.Framework.Assert, nunit.framework", "AreEqual", NUnitStackTraceParser.Attribute)
{
    public readonly static NUnitReporter INSTANCE = new();
}