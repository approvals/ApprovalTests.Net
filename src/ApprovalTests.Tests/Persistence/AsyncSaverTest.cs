[TestFixture]
public class AsyncSaverTest
{
    [Test]
    public void TestAsyncWrapperSave()
    {
        using var f = new TempFile("stuff");
        var s = new FileSaver(f.File);
        ClassicAssert.AreEqual("hello", s.ToAsync().Save("hello").Result);
    }

    [Test]
    public void TestTrueAsyncSave()
    {
        using var f = new TempFile("stuff");
        var s = new FileAsyncSaver(f.File);
        ClassicAssert.AreEqual("hello", s.Save("hello").Result);
    }

    [Test]
    public void TestNonAsyncWrapper()
    {
        using var f = new TempFile("stuff");
        var s = new FileAsyncSaver(f.File);
        ClassicAssert.AreEqual("hello", s.ToSynchronous().Save("hello"));
    }
}