namespace ApprovalTests.Core
{
	public interface IApprovalWriter
	{
		string GetApprovalFilename(string basename);
		string GetReceivedFilename(string basename);
		string WriteReceivedFile(string received);
	}
}