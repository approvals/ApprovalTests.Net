using ApprovalTests.Core;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
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
            Assert.IsTrue(reporter.IsWorkingInThisEnvironment("default.txt"));
            reporter.Report("a", "b");
            Assert.IsNull(a.CalledWith);
            Assert.AreEqual("a,b", b.CalledWith);
            Assert.IsNull(c.CalledWith);
        }

        [Test]
        public void TestException()
        {
            var ex = ExceptionUtilities.GetException(() => new ImageReporter().Report("received.notreal", "received.notreal"));
            Assert.AreEqual("ImageReporter Could not find a Reporter for file received.notreal", ex.Message);
        }
        [Test]
        public void TestCleanup()
        {
            var cleanup1 = new MockCleanup();
            var cleanup2 = new MockCleanup();
            var r = new FirstWorkingReporter(cleanup1, new QuietReporter(), cleanup2);
            r.CleanUp("a", "r");
            Assert.AreEqual("a", cleanup1.approved);
            Assert.AreEqual("a", cleanup2.approved);
        }
    }

    public class MockCleanup : IEnvironmentAwareReporter, IApprovalReporterWithCleanUp
    {
        public string approved;
        private string received;

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
}