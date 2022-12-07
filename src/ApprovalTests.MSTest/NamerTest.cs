using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MsTestV2;

[TestClass]
[UseReporter(typeof(DiffReporter))]
public class NamerTest
{
    [TestMethod]
    public void MsTestV2()
    {
        Approvals.Verify("MsTestV2");
    }

    [DataTestMethod]
    [DataRow("MsDataTest")]
    public void MsTestV2DataTestMethod(string s)
    {
        using (ApprovalResults.ForScenario(s))
        {
            Approvals.Verify(s);
        }
    }
}