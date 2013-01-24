using System.Linq;
using ApprovalUtilities.Persistence.EntityFramework;

namespace ApprovalTests.Tests.EntityFramework
{
	public class CompanyLoaderByName : EntityFrameworkLoader<Company, Company[], ModelContainer>
	{
		private readonly string name;

		public CompanyLoaderByName(string name) : base(() => new ModelContainer())
		{
			this.name = name;
		}

		public override IQueryable<Company> GetLinqStatement()
		{
			return (from c in GetDatabaseContext().Companies
			        where c.Name.StartsWith(name)
			        select c).Take(1);
		}

		public override Company[] Load()
		{
			return GetLinqStatement().ToArray();
		}
	}
}