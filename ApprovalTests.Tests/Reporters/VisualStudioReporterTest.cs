using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    using ApprovalTests.Reporters;

    using Microsoft.Win32;

    [TestFixture]
    public class VisualStudioReporterTest
    {
        [Test]
        public void Demo()
        {
            VisualStudioReporter.INSTANCE.IsWorkingInThisEnvironment("a.txt");
        }
        

    }
}