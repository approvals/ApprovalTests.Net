using System.Data.Entity;
using System.Linq;
using ApprovalTests.Persistence;

namespace ApprovalTests.EntityFramework
{
    public class EntityFrameworkDbContextApprovals
    {
        public static void Verify<T>(DbContext db, IQueryable<T> queryable)
        {
            DatabaseApprovals.Verify(new DbContextAdaptor<T>(db, queryable));
        }
    }
}