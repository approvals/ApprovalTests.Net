[TestClass]
public class MSTestReporterTest
{
    [TestMethod]
    [UseReporter(typeof(MsTestReporter))]
    public void TestReporter()
    {
        Assert.IsTrue(MsTestReporter.INSTANCE.IsWorkingInThisEnvironment("a.txt"));

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