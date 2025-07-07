public class ApprovalsFilenameTest
{
    [Test]
    public void TestMachineSpecificName()
    {
        // begin-snippet: approvals_filename
        var approvalsFilename = ApprovalsFilename.Parse(@"..\Email\EmailTest.Testname.Microsoft_Windows_10_Education.approved.eml");
        // end-snippet
        Approvals.Verify(approvalsFilename);
        ClassicAssert.True(approvalsFilename.IsMachineSpecific);
    }

    [Test]
    public void TestNonMachineSpecificName() =>
        Approvals.Verify(ApprovalsFilename.Parse(@"..\Email\EmailTest.Testname.approved.eml"));

    [Test]
    public void TestSimilarFiles()
    {
        var file = PathUtilities.GetAdjacentFile(@"..\LockDownTests.TestExceptions.Microsoft_Windows_10_Education.approved.txt");
        var approvalsFilename = ApprovalsFilename.Parse(file);
        Approvals.VerifyAll("Like LockDownTests.TestExceptions.Microsoft_Windows_10_Education.approved.txt", approvalsFilename.GetOtherMachineSpecificFiles(), f => f.Name);
    }
}