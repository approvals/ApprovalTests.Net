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
    }
}