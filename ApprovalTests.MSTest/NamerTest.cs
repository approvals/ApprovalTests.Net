using ApprovalTests.Reporters;
using ApprovalUtilities.SimpleLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MSTest
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class NamerTest
	{
		
		[TestMethod]
		public void MSTestVS2010()
		{
			Logger.Variable("a",1);
			Approvals.Verify("2010");
		}
	}
}