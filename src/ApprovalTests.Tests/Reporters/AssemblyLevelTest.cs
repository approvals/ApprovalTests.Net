[TestFixture]
public class AssemblyLevelTest
{
    [Test]
    public void TestClassLevel()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            ClassicAssert.AreEqual(typeof(DiffReporter), Approvals.GetReporter().GetType());
        }
    }
}