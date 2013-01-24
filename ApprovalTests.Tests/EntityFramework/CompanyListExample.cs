using System.Linq;
using System.Text;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Tests.EntityFramework
{
	public class CompanyList
	{
		public static string GetCompanyRoster(string name)
		{
			var companies = (from c in new ModelContainer().Companies
			                 where c.Name.StartsWith(name)
			                 select c).Take(9);
			var b = new StringBuilder();
			b.Append("<html><body>");
			foreach (var company in companies)
			{
				b.Append("<li>{0}</li>".FormatWith(company.Name));
			}
			b.Append("</body></html>");
			return b.ToString();
		}
	}
}