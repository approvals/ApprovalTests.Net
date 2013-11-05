using ApprovalTests.Namers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
	[TestFixture]
	public class SubdirectoryNamerTests
	{
		[Test]
		[UseApprovalSubdirectory("Foo")]
		public void TestSourcePath()
		{
			var name = new UnitTestFrameworkNamer().SourcePath;
			Assert.IsTrue(name.Contains(@"ApprovalTests.Net\ApprovalTests.Tests\Namer\Foo"),name);
		}

	}
}