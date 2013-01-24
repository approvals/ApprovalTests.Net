using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.EntityFramework
{
	public class CompanyListExampleTest
	{
		[TestFixture]
		[UseReporter(typeof (FileLauncherReporter), typeof (DiffReporter))]
		public class CompanyListTest
		{
			[Test]
			public void TestLoader()
			{
			}
		}
	}