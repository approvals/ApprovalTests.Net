using System.Linq;
using System.Reflection;
using ApprovalTests.Persistence.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using Environment = System.Environment;

namespace ApprovalTests.MachineSpecific.Tests.NHibernate
{
	[TestClass]
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

		[TestMethod]
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