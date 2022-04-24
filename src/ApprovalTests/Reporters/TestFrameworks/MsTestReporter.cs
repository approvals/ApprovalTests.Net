
using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Reporters.TestFrameworks;

public class MsTestReporter : AssertReporter
{
    public readonly static MsTestReporter INSTANCE = new();

    public MsTestReporter()
        : base("Microsoft.VisualStudio.TestTools.UnitTesting.Assert, Microsoft.VisualStudio.TestPlatform.TestFramework", "AreEqual", VSStackTraceParser.Attribute)
    {
    }
}