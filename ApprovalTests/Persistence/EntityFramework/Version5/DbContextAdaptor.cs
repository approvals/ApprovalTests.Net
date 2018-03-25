#if NETCORE
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using ApprovalUtilities.Persistence.EntityFramework;
#endif
using ApprovalUtilities.Persistence.Database;
using System.Linq;
using System.Reflection;
using System.Data.Common;

namespace ApprovalTests.Persistence.EntityFramework.Version5
{
    public class DbContextAdaptor<T> : IDatabaseToExecuteableQueryAdaptor where T : class

    {
		private readonly DbContext db;
		private readonly IQueryable<T> queryable;

		public DbContextAdaptor(DbContext db, IQueryable<T> queryable)
		{
			this.db = db;
			this.queryable = queryable;
		}

		public string GetQuery()
		{
#if NETCORE
            return queryable.ToSql();
#else
            DbQuery<T> dbQuery = (DbQuery<T>) queryable;
			return EntityFrameworkUtils.GetQueryFromLinq(GetObjectQuery(dbQuery));
#endif
        }
#if !NETCORE
        public static ObjectQuery<T1> GetObjectQuery<T1>( DbQuery<T1> query)
		{
			var internalQueryField = query.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name.Equals("_internalQuery"));

			var internalQuery = internalQueryField.GetValue(query);

			var objectQueryField = internalQuery.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name.Equals("_objectQuery"));

			var objectQuery = objectQueryField.GetValue(internalQuery) as ObjectQuery<T1>;

			return objectQuery;
		}
#endif
		
		public DbConnection GetConnection()
		{
			return GetConnectionFrom(db);
		}

		public static DbConnection GetConnectionFrom(DbContext context)
		{
#if NETCORE
            return context.Database.GetDbConnection();
#else
            return context.Database.Connection;
#endif

        }

	}


}
