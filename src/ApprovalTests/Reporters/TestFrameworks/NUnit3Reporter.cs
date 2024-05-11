namespace ApprovalTests.Reporters.TestFrameworks;

public class NUnit3Reporter : AssertReporter
{
    public readonly static NUnit3Reporter INSTANCE = new();

    public NUnit3Reporter()
        : base("NUnit.Framework.Assert, nunit.framework", "AreEqual", NUnitStackTraceParser.Attribute)
    {
    }
}