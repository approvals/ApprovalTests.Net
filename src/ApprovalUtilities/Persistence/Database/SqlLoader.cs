using System.Data.Common;

namespace ApprovalUtilities.Persistence.Database;

public abstract class SqlLoader<T> : ILoader<T>, IExecutableQuery
{
    public readonly Func<DbCommand> CommandCreator;
    public readonly string ConnectionString;

    protected SqlLoader(string connectionString) =>
        ConnectionString = connectionString;

    protected SqlLoader(Func<DbCommand> commandCreator) =>
        CommandCreator = commandCreator;

    public abstract T Load();

    public abstract string GetQuery();

    public string ExecuteQuery(string query) =>
        SqlLoaderUtils.ExecuteQueryToDisplayString(query, ConnectionString, CommandCreator);
}