[TestFixture]
public class NUnitReporterTest
{
    [Test]
    public void TestNunitIsWorking()
    {
        Approvals.SetCaller();
        Assert.IsTrue(NUnit3Reporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
    }

    [Test]
    [UseReporter(typeof(NUnitReporterWithCleanup))]
    public void TestReporter()
    {
        try
        {
            using (new TestExecutionContext.IsolatedContext())
            {
                Approvals.Verify("Hello");
            }
        }
        catch (AssertionException exception)
        {
            var expected = string.Format("  String lengths are both 5. Strings differ at index 0.{0}  Expected: \"World\"{0}  But was:  \"Hello\"{0}  -----------^{0}", Environment.NewLine);
            Assert.AreEqual(
                expected,
                exception.Message);
        }
    }
}