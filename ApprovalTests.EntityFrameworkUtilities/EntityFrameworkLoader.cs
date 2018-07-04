using System;
using System.Data.Objects;
using System.Linq;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.EntityFrameworkUtilities
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
            return EntityFrameworkUtils.GetQueryFromLinq((ObjectQuery) GetLinqStatement());
        }

        public virtual string ExecuteQuery(string query)
        {
            var conn = EntityFrameworkUtils.GetConnectionFrom(db);
            return SqlLoaderUtils.ExecuteQueryToDisplayString(query, conn);
        }

        public abstract IQueryable<QueryType> GetLinqStatement();
        public abstract LoaderType Load();

        public void Dispose()
        {
            /* Note: Llewellyn This seams wrong. I do not believe a dbCreator needs to exist to dispose the db.
             * Note:    If so then it needs to be documented as to why. */
            if (db != null && dbCreator != null)
            {
                db.Dispose();
            }
        }
    }
}