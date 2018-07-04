using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.EntityFrameworkUtilities
{
    public class ObjectContextAdaptor<T> : IDatabaseToExecuteableQueryAdaptor
    {
        private readonly ObjectContext db;
        private readonly IQueryable<T> queryable;

        public ObjectContextAdaptor(ObjectContext db, IQueryable<T> queryable)
        {
            this.db = db;
            this.queryable = queryable;
        }

        public string GetQuery()
        {
            return EntityFrameworkUtils.GetQueryFromLinq((ObjectQuery<T>) queryable);
        }

        public DbConnection GetConnection()
        {
            return EntityFrameworkUtils.GetConnectionFrom(db);
        }
    }

    public class EntityFrameworkUtils
    {
        public static DbConnection GetConnectionFrom(ObjectContext context)
        {
            return ((EntityConnection) context.Connection).StoreConnection;
        }

        public static string GetQueryFromLinq(ObjectQuery linq)
        {
            var sql = linq.ToTraceString();
            return linq.Parameters.Aggregate(sql, (current, p) => current.Replace("@" + p.Name, "\'" + p.Value + "\'"));
        }
    }
}