namespace ApprovalTests.Reporters.TestFrameworks;

public class NUnit4Reporter : AssertReporter
{
    public readonly static NUnit4Reporter INSTANCE = new();

    public NUnit4Reporter()
        : base("NUnit.Framework.Legacy.ClassicAssert, nunit.framework.legacy", "AreEqual", NUnitStackTraceParser.Attribute)
    {
    }
}