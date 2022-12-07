namespace ApprovalTests.Tests.Reporters;

[TestFixture]
public class AssemblyLevelTest
{
    [Test]
    public void TestClassLevel()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Assert.AreEqual(typeof(DiffReporter), Approvals.GetReporter().GetType());
        }
    }


}