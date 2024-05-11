[TestFixture]
[UseReporter(typeof(QuietReporter))]
public class ExecutableTest
{
    static List<string> RunExecutableApproval()
    {
        var output = new List<string>();

        try
        {
            NamerFactory.AdditionalInformation = "Inner";
            Approvals.VerifyWithCallback("Sam", s => output.Add(s));
        }
        catch (Exception)
        {
        }

        return output;
    }

    [Test]
    public void TestExecutableFailure()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
        }
    }

    [Test]
    public void TestExecutableFailureWithPreviousApproval()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
        }
    }

    [Test]
    public void TestExecutableSuccess()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
        }
    }
}