namespace ApprovalTests.Core
{
	public interface IApprovalApprover
	{
		bool Approve();
		void Fail();
		void ReportFailure(IApprovalFailureReporter reporter);
		void CleanUpAfterSuccess(IApprovalFailureReporter reporter);
	}
}