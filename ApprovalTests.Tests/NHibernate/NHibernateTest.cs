using System.Reflection;
using ApprovalTests.Persistence.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using System.Linq;

namespace ApprovalTests.Tests.NHibernate
{
	[TestFixture]
	public class NHibernateTest
	{
		[Test]
		public void TestSimpleQuery()
		{
			using (ISession session = OpenSession())
			{
				IQueryable<Company> query =
					from a in session.Query<Company>()
					where a.Name.StartsWith("Mic")
					select a;
				NHibernateApprovals.Verify((NhQueryable<Company>)query);
			}
		}

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
	}
}