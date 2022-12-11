#if(NET48)
using ApprovalTests.Maintenance;

[TestFixture]
public class RunMaintenance
{
    [Test]
    public void EnsureNoAbandonedFiles() =>
        ApprovalMaintenance.VerifyNoAbandonedFiles(
            "CustomNamerShouldBeSubstitutable.approved.txt",
            "StringEncodingTest.TestUnicode.approved.txt",
            "AsyncTests.TestAsyncExceptionFromVoid",
            "VerifyExceptionWithStacktrace"
        );
}
#endif