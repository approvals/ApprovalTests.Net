using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.Persistence;

public class ExecutableSqlQuery : IExecutableQuery
{
    readonly IDatabaseToExecutableQueryAdapter adapter;

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