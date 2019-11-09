using ApprovalUtilities.Obsolete;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.Persistence
{
    public class ExecutableSqlQuery : IExecutableQuery
    {
        private readonly IDatabaseToExecutableQueryAdapter adapter;

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "ExecutableSqlQuery(IDatabaseToExecutableQueryAdapter)")]
        public ExecutableSqlQuery(IDatabaseToExecuteableQueryAdaptor adapter)
        {
            this.adapter = adapter;
        }

        public ExecutableSqlQuery(IDatabaseToExecutableQueryAdapter adapter)
        {
            this.adapter = adapter;
        }

        public string GetQuery()
        {
            return adapter.GetQuery();
        }

        public string ExecuteQuery(string query)
        {
            return SqlLoaderUtils.ExecuteQueryToDisplayString(query, adapter.GetConnection());
        }
    }
}