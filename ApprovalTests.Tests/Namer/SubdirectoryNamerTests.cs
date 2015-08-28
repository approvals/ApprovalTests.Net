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
			var expectedPath = @"ApprovalTests.Net\ApprovalTests.Tests\Namer\Foo".Replace(@"\", System.IO.Path.DirectorySeparatorChar.ToString());
			StringAssert.Contains(expectedPath, name);
		}

	}
}