using System;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using ApprovalUtilities.Persistence.Database;

namespace ApprovalUtilities.Persistence.EntityFramework
{
	public abstract class EntityFrameworkLoader<QueryType, LoaderType, DatabaseContextType> : IExecutableLoader<LoaderType>,
																							  IDisposable
		where DatabaseContextType : ObjectContext
	{
		private readonly Func<DatabaseContextType> dbCreator;
		private DatabaseContextType db;

		protected EntityFrameworkLoader(Func<DatabaseContextType> dbCreator)
		{
			this.dbCreator = dbCreator;
		}

		protected EntityFrameworkLoader(DatabaseContextType nonDisposableDatabaseContext)
		{
			db = nonDisposableDatabaseContext;
		}

		public DatabaseContextType GetDatabaseContext()
		{
			return db ?? (db = dbCreator());
		}

		public string GetQuery()
		{
			var linq = ((ObjectQuery)GetLinqStatement());
			var sql = linq.ToTraceString();
			return linq.Parameters.Aggregate(sql, (current, p) => current.Replace("@" + p.Name, "\'" + p.Value + "\'"));
		}

		public virtual string ExecuteQuery(string query)
		{
			var conn = (SqlConnection)((EntityConnection)db.Connection).StoreConnection;
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			return SqlLoaderUtils.ExecuteQueryToDisplayString(query, null, conn.CreateCommand);
		}

		public abstract IQueryable<QueryType> GetLinqStatement();
		public abstract LoaderType Load();

		public void Dispose()
		{
			/* Note: Llewellyn This seams wrong. I do not believe a dbCreator needs to exist to dispose the db.
			 * Note:    If so then it needs to be documented as to why.
			 */
			if (db != null && dbCreator != null)
			{
				db.Dispose();
			}
		}
	}
}