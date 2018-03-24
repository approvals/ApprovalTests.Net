using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using Environment = System.Environment;

namespace ApprovalTests.NHibernate.Tests
{
    [TestFixture]
    public class NHibernateTest
    {
        public static ISessionFactory SessionFactory;

        public static ISession OpenSession()
        {
            if (SessionFactory == null) //not threadsafe
            {
                //SessionFactories are expensive, create only once
                Configuration configuration = new Configuration();
                configuration.AddAssembly(Assembly.GetCallingAssembly());
                SessionFactory = configuration.BuildSessionFactory();
            }

            return SessionFactory.OpenSession();
        }

        [Test]
        public void TestSimpleQuery()
        {
            if (!Environment.MachineName.ToLowerInvariant().Contains("llewellyn"))
            {
                //Only works on Llewellyn's machine.
                return;
            }

            using (ISession session = OpenSession())
            {
                IQueryable<Company> query =
                    from a in session.Query<Company>()
                    where a.Name.StartsWith("Mic")
                    select a;
                NHibernateApprovals.Verify((NhQueryable<Company>) query);
            }
        }
    }
}