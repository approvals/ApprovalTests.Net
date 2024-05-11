#if(NET48)
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