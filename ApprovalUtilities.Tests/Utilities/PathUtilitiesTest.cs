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
			var found = PathUtilities.LocateFileFromEnvironmentPath("ipconfig.exe").FirstOrDefault();
			AssertEqualIgnoreCase(@"C:\Windows\System32\ipconfig.exe", found);
		}

		private void AssertEqualIgnoreCase(string expected, string actual)
		{
			Assert.AreEqual(expected.ToLowerInvariant(), actual.ToLowerInvariant());
		}

		[TestMethod]
		public void TestFindsMultipleFiles()
		{
			Approvals.VerifyAll(PathUtilities.LocateFileFromEnvironmentPath("notepad.exe").Select(f=>f.ToLowerInvariant()), "Found");
		}

		[TestMethod]
		public void TestDoesNotFindFile()
		{
			string noneExistingFile = "ThisFileShouldNotExist.exe";
			var results = PathUtilities.LocateFileFromEnvironmentPath(noneExistingFile);
			Assert.AreEqual(0,results.Count());
		}
	}
}