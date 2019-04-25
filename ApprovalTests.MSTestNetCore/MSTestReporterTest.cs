using System;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MSTestNetCore
{
    [TestClass]
    public class MsTestReporterTest
    {
        [TestMethod]
        [UseReporter(typeof(MsTestReporter))]
        public void TestReporter()
        {
            try
            {
                Approvals.Verify("Hello");
                Assert.Fail("Above verification should have thrown an AssertFailedException");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Assert.AreEqual failed. Expected:<World>. Actual:<Hello>. ", e.Message);
            }
        }
    }
}
