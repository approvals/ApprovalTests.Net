
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MSTest
{
	[TestClass]

	[UseReporter(typeof(DiffReporter))]
	public class EF5Test
	{

//		[TestMethod]
//		public void FromIQueryable()
//		{
//			using (var db = new EntityFrameworkDemoEntities())
//			{
//			    EntityFrameworkDbContextApprovals.Verify(db, CreateCompanyLoaderByName2(db, "Mic"));
//			}
//		}
//
//		private IQueryable<Company> CreateCompanyLoaderByName2(EntityFrameworkDemoEntities db, string name)
//		{
//			return (from c in db.Companies
//			        where c.Name.StartsWith(name)
//			        select c).Take(10);
//		}
	}
} 
