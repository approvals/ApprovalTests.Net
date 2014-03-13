using ApprovalTests.Namers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
	[TestFixture]
	public class ApprovalResultsTest
	{
		[Test]
		public void TestEasyNames()
		{
			Assert.AreEqual("Windows 7", ApprovalResults.TransformEasyOsName("Microsoft Windows 7 Professional N"));
		}
	}
}