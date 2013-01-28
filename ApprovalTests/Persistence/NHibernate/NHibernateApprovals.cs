using ApprovalUtilities.Persistence.NHibernate;
using NHibernate.Linq;

namespace ApprovalTests.Persistence.NHibernate
{
	public class NHibernateApprovals
	{
		public static void Verify<T>(NhQueryable<T> queryable)
		{
			DatabaseApprovals.Verify(new NhQueryableAdaptor<T>(queryable));
		}
	}
}