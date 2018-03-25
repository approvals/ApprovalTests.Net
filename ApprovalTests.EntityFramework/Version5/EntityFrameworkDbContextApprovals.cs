using System.Data.Entity;
using System.Linq;
using ApprovalTests.Persistence;

namespace ApprovalTests.EntityFramework.Version5
{
	public class EntityFrameworkDbContextApprovals
	{
		public static void Verify<T>(DbContext db, IQueryable<T> queryable) where T:class
		{
			DatabaseApprovals.Verify(new DbContextAdaptor<T>(db, queryable));
		} 
	}
}