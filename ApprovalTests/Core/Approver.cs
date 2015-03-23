namespace ApprovalTests.Core
{
    public class Approver
    {
        public static void Verify(IApprovalApprover approver, IApprovalFailureReporter reporter)
        {
            if (approver.Approve())
                approver.CleanUpAfterSuccess(reporter);
            else
            {
                approver.ReportFailure(reporter);

                if (reporter is IReporterWithApprovalPower && ((IReporterWithApprovalPower)reporter).ApprovedWhenReported())
                    approver.CleanUpAfterSuccess(reporter);
                else
                    approver.Fail();
            }
        }
    }
}