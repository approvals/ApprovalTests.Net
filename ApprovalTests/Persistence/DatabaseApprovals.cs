using ApprovalTests.Core;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.Database;
using ApprovalUtilities.Persistence.EntityFramework;

namespace ApprovalTests.Persistence
{
	public class DatabaseApprovals
	{
		public static void Verify(IDatabaseToExecuteableQueryAdaptor adaptor)
		{
			Approvals.Verify(new ExecutableSqlQuery(adaptor));	}
	}

	public class ExecutableSqlQuery : IExecutableQuery
	{
		private readonly IDatabaseToExecuteableQueryAdaptor adaptor;

		public ExecutableSqlQuery(IDatabaseToExecuteableQueryAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		public string GetQuery()
		{
			return adaptor.GetQuery();
		}

		public string ExecuteQuery(string query)
		{

			return SqlLoaderUtils.ExecuteQueryToDisplayString(query, adaptor.GetConnection());
		}
	}
}