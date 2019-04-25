using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Reporters
{
    public class MsTestReporter : AssertReporter
    {
        public readonly static MsTestReporter INSTANCE = new MsTestReporter();

        public MsTestReporter()
            : base("Microsoft.VisualStudio.TestTools.UnitTesting.Assert", "AreEqual", VSStackTraceParser.Attribute,
              "Microsoft.VisualStudio.QualityTools.UnitTestFramework", "Microsoft.VisualStudio.TestPlatform.TestFramework")
        {
        }
    }
}