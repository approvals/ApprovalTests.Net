using System.Data.Objects;
using System.Linq;
using ApprovalUtilities.Persistence.EntityFramework;

namespace ApprovalTests.Persistence.EntityFramework
{
	public class EntityFrameworkApprovals
	{
		public static void Verify<T>(ObjectContext db, IQueryable<T> queryable)
		{
			Approvals.Verify(new LambdaEnumerableLoader<T,ObjectContext>(db,_=> queryable));
		}
	}
}