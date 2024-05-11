using NUnit.Framework.Internal;

[TestFixture]
public class NUnitReporterTest
{
    [Test]
    public void TestNunitIsWorking()
    {
        Approvals.SetCaller();
        ClassicAssert.IsTrue(NUnit4Reporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
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
        catch (Exception e)
        {
            var expectedMessage = string.Format("  Assert.That(actual, Is.EqualTo(expected)){0}  String lengths are both 5. Strings differ at index 0.{0}  Expected: \"World\"{0}  But was:  \"Hello\"{0}  -----------^{0}", Environment.NewLine);
            ClassicAssert.AreEqual(expectedMessage, e.Message);
        }
    }
}