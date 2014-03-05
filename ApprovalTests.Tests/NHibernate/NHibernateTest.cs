using System.Linq;
using System.Reflection;
using ApprovalTests.Persistence.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;

namespace ApprovalTests.Tests.NHibernate
{
    using System;

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
            if (Environment.MachineName != "LLewellyn's machine")
            {
                Assert.Ignore("Only works on Llewellyn's machine.");
            }

            using (ISession session = OpenSession())
            {
                IQueryable<Company> query =
                    from a in session.Query<Company>()
                    where a.Name.StartsWith("Mic")
                    select a;
                NHibernateApprovals.Verify((NhQueryable<Company>)query);
            }
        }
    }
}