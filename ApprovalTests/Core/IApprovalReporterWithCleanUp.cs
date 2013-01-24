namespace ApprovalTests.Core
{
	public interface IApprovalReporterWithCleanUp
	{
		void CleanUp(string approved, string received);
	}
}