using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.Persistence
{
    public class ExecutableSqlQuery : IExecutableQuery
    {
        private readonly IDatabaseToExecuteableQueryAdaptor adapter;

        public ExecutableSqlQuery(IDatabaseToExecuteableQueryAdaptor adapter)
        {
            this.adapter = adapter;
        }

        public string GetQuery()
        {
            return this.adapter.GetQuery();
        }

        public string ExecuteQuery(string query)
        {
            return SqlLoaderUtils.ExecuteQueryToDisplayString(query, this.adapter.GetConnection());
        }
    }
}