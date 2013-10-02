using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.Persistence.Database;
using ApprovalUtilities.Persistence.EntityFramework;

namespace ApprovalTests.Persistence.EntityFramework.Version5
{
	public class DbContextAdaptor<T> : IDatabaseToExecuteableQueryAdaptor
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
			DbQuery<T> dbQuery = (DbQuery<T>) queryable;
			return EntityFrameworkUtils.GetQueryFromLinq(GetObjectQuery(dbQuery));
		}
		public static ObjectQuery<T1> GetObjectQuery<T1>( DbQuery<T1> query)
		{
			var internalQueryField = query.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name.Equals("_internalQuery"));

			var internalQuery = internalQueryField.GetValue(query);

			var objectQueryField = internalQuery.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name.Equals("_objectQuery"));

			var objectQuery = objectQueryField.GetValue(internalQuery) as ObjectQuery<T1>;

			return objectQuery;
		}

		
		public DbConnection GetConnection()
		{
			return GetConnectionFrom(db);
		}

		public static DbConnection GetConnectionFrom(DbContext context)
		{
			return context.Database.Connection;
		}

	}
}