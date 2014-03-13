using NUnit.Framework;

namespace ApprovalTests.Tests
{
	[TestFixture]
	public class StackTraceScrubberTest
	{
		[Test]
		public void TestDashedPath()
		{
			var path = @"C:\code\ApprovalTests - Net\Persistence\Datasets\DatasetTest.cs";
			Assert.AreEqual("...\\DatasetTest.cs", StackTraceScrubber.ScrubPaths(path));
		}
	}
}