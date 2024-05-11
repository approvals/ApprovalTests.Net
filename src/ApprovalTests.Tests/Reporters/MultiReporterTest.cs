[TestFixture]
public class MultiReporterTest
{
    [Test]
    public void TestMultiReporter()
    {
        var a = new RecordingReporter();
        var b = new RecordingReporter();
        var multi = new MultiReporter(a, b);
        multi.Report("a", "r");
        ClassicAssert.AreEqual("a,r", a.CalledWith);
        ClassicAssert.AreEqual("a,r", b.CalledWith);
    }

    [Test]
    public void TestCallAfterException()
    {
        var a = new NUnit4Reporter();
        var b = new RecordingReporter();
        var multi = new MultiReporter(a, b);
        var exception = ExceptionUtilities.GetException(() => multi.Report("a", "r"));
        ClassicAssert.AreEqual("a,r", b.CalledWith);
        ClassicAssert.IsInstanceOf<Exception>(exception);
    }
}