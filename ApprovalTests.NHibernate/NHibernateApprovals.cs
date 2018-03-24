using ApprovalTests.Persistence;
using NHibernate.Linq;

namespace ApprovalTests.NHibernate
{
    public class NHibernateApprovals
    {
        public static void Verify<T>(NhQueryable<T> queryable)
        {
            DatabaseApprovals.Verify(new NhQueryableAdaptor<T>(queryable));
        }
    }
}