using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MsTestV2
{
    [TestClass]
	[UseReporter(typeof(DiffReporter))]
    public class NamerTest
    {
		[TestMethod]
		public void MSTestV2()
		{
			Approvals.Verify("MsTestV2");
		}

		[DataTestMethod]
        [DataRow("DataTestMethod")]
		public void MSTestV2DataTestMethod(string s)
		{
			Approvals.Verify(s);
		}
    }
}
