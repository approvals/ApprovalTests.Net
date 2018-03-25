using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Persistence
{
    [TestFixture]
    public class AsyncSaverTest
    {
        [Test]
        public void TestAnsyncWrapperSave()
        {
            using (var f = new TempFile("stuff"))
            {
                var s = new FileSaver(f.File);
                Assert.AreEqual("hello", s.ToAsync().Save("hello").Result);
            }
        }

        [Test]
        public void TestTrueAnsyncSave()
        {
            using (var f = new TempFile("stuff"))
            {
                var s = new FileAsyncSaver(f.File);
                Assert.AreEqual("hello", s.Save("hello").Result);
            }
        }

        [Test]
        public void TestNonAnsyncWrapper()
        {
            using (var f = new TempFile("stuff"))
            {
                var s = new FileAsyncSaver(f.File);
                Assert.AreEqual("hello", s.ToSynchronous().Save("hello"));
            }
        }
    }
}