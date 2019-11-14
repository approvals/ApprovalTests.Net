using ApprovalTests.Reporters.Windows;
using NUnit.Framework;

namespace ApprovalTests.MachineSpecific.Tests
{
    [TestFixture]
    public class RiderReporterTest
    {
        [Test]
        [Ignore("Only works when rider is installed")]
        public void WhenLaunchedFromVisualStudioThenIsWorkingInThisEnvironmentForTextFiles()
        {
            var riderReporter = new RiderReporter();
            Assert.IsTrue(riderReporter.IsWorkingInThisEnvironment("someFile.txt"));
        }
    }
}