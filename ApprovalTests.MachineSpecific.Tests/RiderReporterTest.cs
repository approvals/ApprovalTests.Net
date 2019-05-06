using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.MachineSpecific.Tests
{
    [TestFixture]
    public class RiderReporterTest
    {
        [Test]
        public void WhenLaunchedFromVisualStudioThenIsWorkingInThisEnvironmentForTextFiles()
        {
            var riderReporter = new RiderReporter();
            Assert.IsTrue(riderReporter.IsWorkingInThisEnvironment("someFile.txt"));
        }
    }
}