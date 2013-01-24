namespace ApprovalUtilities.Persistence
{
	public interface IExecutableQuery
	{
		string GetQuery();
		string ExecuteQuery(string query);
	}

	public interface IExecutableLoader<T> : IExecutableQuery, ILoader<T>
	{

	}
}