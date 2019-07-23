using ApprovalTests.Namers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
    public class ApprovalsFilenameTest
    {
        [Test]
        public void TestMachineSpecificName()
        {
            var approvalsFilename = ApprovalsFilename.Parse(@"C:\Users\olgica\Documents\GitHub\ApprovalTests.Net\ApprovalTests.Tests\Email\EmailTest.Testname.Microsoft_Windows_10_Education.approved.eml");
            Approvals.Verify(approvalsFilename);
            Assert.True(approvalsFilename.IsMachineSpecific);
        }

        [Test]
        public void TestNonMachineSpecificName()
        {
            Approvals.Verify(ApprovalsFilename.Parse(@"C:\Users\olgica\Documents\GitHub\ApprovalTests.Net\ApprovalTests.Tests\Email\EmailTest.Testname.approved.eml"));
        }
    }
}