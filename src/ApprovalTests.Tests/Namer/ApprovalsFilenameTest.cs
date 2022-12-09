public class ApprovalsFilenameTest
{
    [Test]
    public void TestMachineSpecificName()
    {
        // begin-snippet: approvals_filename
        var approvalsFilename = ApprovalsFilename.Parse(@"..\Email\EmailTest.Testname.Microsoft_Windows_10_Education.approved.eml");
        // end-snippet
        Approvals.Verify(approvalsFilename);
        Assert.True(approvalsFilename.IsMachineSpecific);
    }

    [Test]
    public void TestNonMachineSpecificName()
    {
        Approvals.Verify(ApprovalsFilename.Parse(@"..\Email\EmailTest.Testname.approved.eml"));
    }

    [Test]
    public void TestSimilarFiles()
    {
        var file = PathUtilities.GetAdjacentFile(@"..\Email\EmailTest.Testname.Microsoft_Windows_10_Education.approved.eml");
        var approvalsFilename = ApprovalsFilename.Parse(file);
        Approvals.VerifyAll("Like EmailTest.Testname.Microsoft_Windows_10_Education.approved.eml", approvalsFilename.GetOtherMachineSpecificFiles(), f => f.Name);
    }
}