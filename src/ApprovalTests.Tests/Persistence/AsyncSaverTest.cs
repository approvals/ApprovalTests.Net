using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Persistence;

[TestFixture]
public class AsyncSaverTest
{
    [Test]
    public void TestAsyncWrapperSave()
    {
        using var f = new TempFile("stuff");
        var s = new FileSaver(f.File);
        Assert.AreEqual("hello", s.ToAsync().Save("hello").Result);
    }

    [Test]
    public void TestTrueAsyncSave()
    {
        using var f = new TempFile("stuff");
        var s = new FileAsyncSaver(f.File);
        Assert.AreEqual("hello", s.Save("hello").Result);
    }

    [Test]
    public void TestNonAsyncWrapper()
    {
        using var f = new TempFile("stuff");
        var s = new FileAsyncSaver(f.File);
        Assert.AreEqual("hello", s.ToSynchronous().Save("hello"));
    }
}