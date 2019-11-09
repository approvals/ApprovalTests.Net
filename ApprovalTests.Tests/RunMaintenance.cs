using ApprovalTests.Maintenance;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    public class RunMaintenance
    {
        [Test]
        public void EnsureNoAbandonedFiles()
        {
            ApprovalMaintenance.VerifyNoAbandonedFiles(
                    "CustomNamerShouldBeSubstitutable.approved.txt",
                    "StringEncodingTest.TestUnicode.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Mac.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Microsoft_Windows_10_Education.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Microsoft_Windows_10_Home_N.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Microsoft_Windows_10_Pro.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Microsoft_Windows_Server_2016_Datacenter.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Microsoft_Windows_Server_2019_Datacenter.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Windows_7.approved.txt",
                    "AsyncTests.TestAsyncExceptionFromVoid.Windows_8.approved.txt"
                );
        }
    }
}