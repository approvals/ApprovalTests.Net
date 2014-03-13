
namespace ApprovalTests.Core
{
	public class Approver
	{
		public static void Verify(IApprovalApprover approver, IApprovalFailureReporter reporter)
		{
			if (approver.Approve())
				approver.CleanUpAfterSucess(reporter);
			else
			{
				approver.ReportFailure(reporter);

				if (reporter is IReporterWithApprovalPower && ((IReporterWithApprovalPower)reporter).ApprovedWhenReported())
					approver.CleanUpAfterSucess(reporter);
				else
					approver.Fail();
			}
		}
	}
}