using NUnit.Framework;

namespace ApprovalTests.EntityFramework.Tests
{
	[TestFixture]
	[Platform(Exclude="Mono")]
	//[UseReporter(typeof(FileLauncherReporter))]
	public class CompanyListTest
	{
		[Test]
		public void TestLoader()
		{
			Approvals.Verify(CompanyList.GetCompanyByName("m"));
		}
		[Test]
		public void TestLoader2()
		{
			Approvals.Verify(CompanyList.GetCompanyByName("mi"));
		}
	}
}