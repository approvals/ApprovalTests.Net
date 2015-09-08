using System.Linq;
using ApprovalTests.Persistence.EntityFramework;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.EntityFramework;
using NUnit.Framework;

namespace ApprovalTests.Tests.EntityFramework
{
	[TestFixture]
	[Platform(Exclude="Mono")]
	public class EntityFrameworkLoaderTest
	{
		[Test]
		public void FromInheritence()
		{
			Approvals.Verify(new CompanyLoaderByName2("M1"));
		}

		[Test]
		public void FromLambda()
		{
			Approvals.Verify(CreateCompanyLoaderByName("Mic"));
		}


		private LambdaEnumerableLoader<Company, ModelContainer> CreateCompanyLoaderByName(string name)
		{
			return LoaderUtils.Load(db => (from c in db.Companies
			                               where c.Name.StartsWith(name)
			                               select c).Take(10));
		}

		[Test]
		public void FromIQueryable()
		{
			using (var db = new ModelContainer())
			{
				EntityFrameworkApprovals.Verify(db, CreateCompanyLoaderByName2(db, "Mic"));
			}
		}

		private IQueryable<Company> CreateCompanyLoaderByName2(ModelContainer db, string name)
		{
			return (from c in db.Companies
			        where c.Name.StartsWith(name)
			        select c).Take(10);
		}

		[Test]
		public void FromSingleLambda()
		{
			var loader1 = CreateCompanyLoaderByName("Mic");
			IExecutableLoader<Company> loader = loader1.Singleton();
			Approvals.Verify(loader);
		}
	}
}