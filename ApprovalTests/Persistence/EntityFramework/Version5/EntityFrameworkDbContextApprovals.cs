#if NETCORE
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif
using System.Linq;

namespace ApprovalTests.Persistence.EntityFramework.Version5
{
	public class EntityFrameworkDbContextApprovals
	{
		public static void Verify<T>(DbContext db, IQueryable<T> queryable) where T:class
		{
			DatabaseApprovals.Verify(new DbContextAdaptor<T>(db, queryable));
		} 
	}
}