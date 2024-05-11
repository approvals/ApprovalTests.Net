[TestFixture]
public class FirstWorkingReporterTest
{
    [Test]
    public void TestCallsFirstAndOnlyFirst()
    {
        var a = new RecordingReporter(false);
        var b = new RecordingReporter(true);
        var c = new RecordingReporter(true);

        var reporter = new FirstWorkingReporter(a, b, c);
        ClassicAssert.IsTrue(reporter.IsWorkingInThisEnvironment("default.txt"));
        reporter.Report("a", "b");
        ClassicAssert.IsNull(a.CalledWith);
        ClassicAssert.AreEqual("a,b", b.CalledWith);
        ClassicAssert.IsNull(c.CalledWith);
    }

    [Test]
    public void TestException()
    {
        if (DiffEngine.BuildServerDetector.Detected)
        {
            // DiffReporter not detected on CI
            return;
        }
        File.Create("received.notreal").Close();
        try
        {
            var ex = ExceptionUtilities.GetException(() => new DiffReporter().Report("received.notreal", "received.notreal"));
            ClassicAssert.AreEqual("Could not find a diff tool for extension: .notreal", ex.Message);
        }
        finally
        {
            File.Delete("received.notreal");
        }
    }

    [Test]
    public void TestCleanup()
    {
        var cleanup1 = new MockCleanup();
        var cleanup2 = new MockCleanup();
        var r = new FirstWorkingReporter(cleanup1, new QuietReporter(), cleanup2);
        r.CleanUp("a", "r");
        ClassicAssert.AreEqual("a", cleanup1.approved);
        ClassicAssert.AreEqual("a", cleanup2.approved);
        ClassicAssert.AreEqual("r", cleanup1.received);
        ClassicAssert.AreEqual("r", cleanup2.received);
    }
}

public class MockCleanup : IEnvironmentAwareReporter, IApprovalReporterWithCleanUp
{
    public string approved;
    public string received;

    public void Report(string approved, string received)
    {
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        return false;
    }

    public void CleanUp(string approved, string received)
    {
        this.approved = approved;
        this.received = received;
    }
}