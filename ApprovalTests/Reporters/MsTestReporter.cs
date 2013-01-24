
using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Reporters
{
	public class MsTestReporter : AssertReporter
	{
		public readonly static MsTestReporter INSTANCE = new MsTestReporter();
		public MsTestReporter()
			: base("Microsoft.VisualStudio.TestTools.UnitTesting.Assert, Microsoft.VisualStudio.QualityTools.UnitTestFramework", "AreEqual", VSStackTraceParser.Attribute)
		{

		}

	}
}
