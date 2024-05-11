namespace ApprovalTests.Reporters.TestFrameworks;

public class NUnit3Reporter() :
    AssertReporter(
        "NUnit.Framework.Assert, nunit.framework",
        "AreEqual",
        NUnitStackTraceParser.Attribute)
{
    public readonly static NUnit3Reporter INSTANCE = new();
}