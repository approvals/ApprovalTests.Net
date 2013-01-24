namespace ApprovalTests.Core
{
	public interface IApprovalNamer
	{
		string SourcePath { get; }
		string Name { get; }
	}
}