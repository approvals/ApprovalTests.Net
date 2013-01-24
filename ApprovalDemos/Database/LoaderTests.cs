using ApprovalDemos.Database.Loaders;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalDemos.Data.Database
{
	[TestFixture]
	[UseReporter(typeof(BeyondCompareReporter))]
	public class LoaderTests
	{

		[Test]
		public void TestInsultLoader()
		{
			string conn = @"server=.\sqlexpress;database=Insults;trusted_connection=true";
			Approvals.Verify(new InsultLoaderShortAndSweet(7, 5, conn));
		}
	}
}