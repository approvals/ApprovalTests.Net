namespace ApprovalTests.Core
{
    public class Approver
    {
        public static void Verify(IApprovalApprover approver, IApprovalFailureReporter reporter)
        {
            if (approver.Approve())
            {
                approver.CleanUpAfterSuccess(reporter);
            }
            else
            {
                approver.ReportFailure(reporter);

                var power = reporter as IReporterWithApprovalPower;
                if (power != null && power.ApprovedWhenReported())
                {
                    approver.CleanUpAfterSuccess(power);
                }
                else
                {
                    approver.Fail();
                }
            }
        }
    }
}