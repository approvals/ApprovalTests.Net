using ApprovalUtilities.Persistence;

namespace ApprovalDemos.Data
{
	public interface IAdoLoader<T> : ILoader<T>
	{
		string Query { get; }
		string ConnectionString { get; }
	}
}