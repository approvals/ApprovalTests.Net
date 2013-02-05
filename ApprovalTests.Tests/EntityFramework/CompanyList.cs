using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Persistence.EntityFramework;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Tests.EntityFramework
{
    public class CompanyList
    {
        public static string GetCompanyRoster(string name)
        {
					  
            return GetCompanyRoster(GetCompanyByName(name));
        }

        public static string GetCompanyRoster(ILoader<IEnumerable<Company>> companyByName)
        {
            IEnumerable<Company> companies = companyByName.Load();
            var b = new StringBuilder();
            b.Append("<html><body>");
            foreach (Company company in companies)
            {
                b.Append("<li>{0}</li>".FormatWith(company.Name));
            }
            b.Append("</body></html>");
            return b.ToString();
        }

        public static LambdaEnumerableLoader<Company, ModelContainer> GetCompanyByName(string name)
        {
            return Loaders.Create(() => new ModelContainer(),
                                  (m) => (from c in m.Companies
                                          where c.Name.StartsWith(name)
                                          select c).Take(9));
        }
    }
}