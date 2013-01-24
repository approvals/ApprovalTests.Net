namespace ApprovalTests.Core
{
	public interface IReporterWithApprovalPower : IApprovalFailureReporter
	{
		bool ApprovedWhenReported();
		
	}
}