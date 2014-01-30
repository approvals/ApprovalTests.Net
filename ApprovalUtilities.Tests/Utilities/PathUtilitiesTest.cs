using System.Linq;
using ApprovalTests;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.Utilities
{
	[TestClass]
	public class PathUtilitiesTest
	{
		[TestMethod]
		public void ScrubPathTest()
		{
			var dir = PathUtilities.GetDirectoryForCaller();
			var file = dir + "PathUtilitiesTest.cs";
			AssertEqualIgnoreCase(@"...\PathUtilitiesTest.cs", PathUtilities.ScrubPath(file, dir));
		}

		[TestMethod]
		public void TestFindsFile()
		{
			var found = PathUtilities.LocateFileFromEnviormentPath(@"ipconfig.exe").FirstOrDefault();
			AssertEqualIgnoreCase(@"C:\Windows\System32\ipconfig.exe", found);
		}

		private void AssertEqualIgnoreCase(string expected, string actual)
		{
			Assert.AreEqual(expected.ToLowerInvariant(), actual.ToLowerInvariant());
		}

		[TestMethod]
		public void TestFindsMultipleFiles()
		{
			Approvals.VerifyAll(PathUtilities.LocateFileFromEnviormentPath(@"notepad.exe").Select(f=>f.ToLowerInvariant()), "Found");
		}

		[TestMethod]
		public void TestDoesNotFindFile()
		{
			var results = PathUtilities.LocateFileFromEnviormentPath(@"ThisFileShouldNotExist.exe");
			Assert.AreEqual(0,results.Count());
		}
	}
}