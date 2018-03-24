using System;
using System.Data.Common;

namespace ApprovalUtilities.Persistence.Database
{
    public abstract class SqlLoader<T> : ILoader<T>, IExecutableQuery
    {
        public readonly Func<DbCommand> CommandCreator;
        public readonly string ConnectionString;

        protected SqlLoader(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected SqlLoader(Func<DbCommand> commandCreator)
        {
            this.CommandCreator = commandCreator;
        }

        public abstract T Load();

        public abstract string GetQuery();

        public string ExecuteQuery(string query)
        {
            return SqlLoaderUtils.ExecuteQueryToDisplayString(query, ConnectionString, CommandCreator);
        }
    }
}