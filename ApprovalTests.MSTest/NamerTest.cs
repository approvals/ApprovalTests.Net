using ApprovalTests.Reporters;
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
            Approvals.Verify("2010");
        }
    }
}