using ApprovalTests;

[TestFixture]
[UseReporter(typeof(MachineSpecificReporter))]
public class ExceptionTests
{
#if NET6_0_OR_GREATER

    [Test]
    public void VerifyExceptionWithStacktrace()
    {
        using (ApprovalResults.UniqueForOs())
        {
            Action wrapper = () => throw new("https://github.com/approvals/ApprovalTests.Net/issues/242");
            var e = ExceptionUtilities.GetException(wrapper);
            Approvals.VerifyExceptionWithStacktrace(e);
        }
    }

#endif

    [Test]
    public void VerifyException()
    {
        Action wrapper = () => throw new("The Message");
        var e = ExceptionUtilities.GetException(wrapper);
        Approvals.VerifyException(e);
    }
}