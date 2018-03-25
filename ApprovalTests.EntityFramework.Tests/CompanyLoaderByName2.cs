using System.Linq;

namespace ApprovalTests.EntityFramework.Tests
{
	public class CompanyLoaderByName2 : MultiLoader<Company>
	{
		private readonly string name;

		public CompanyLoaderByName2(string name)
		{
			this.name = name;
		}

		public override IQueryable<Company> GetLinqStatement()
		{
			return (from c in GetDatabaseContext().Companies
			        where c.Name.StartsWith(name)
			        select c).Take(1);
		}
	}
}