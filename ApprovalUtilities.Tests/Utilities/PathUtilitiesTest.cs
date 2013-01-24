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
			Assert.AreEqual(@"...\PathUtilitiesTest.cs", PathUtilities.ScrubPath(file, dir));
		}

		[TestMethod]
		public void TestFindsFile()
		{
			Assert.AreEqual(@"C:\Windows\System32\ipconfig.exe",
			                PathUtilities.LocateFileFromEnviormentPath(@"ipconfig.exe").FirstOrDefault());
		}

		[TestMethod]
		public void TestFindsMultipleFiles()
		{
			Approvals.VerifyAll(PathUtilities.LocateFileFromEnviormentPath(@"notepad.exe"), "Found");
		}

		[TestMethod]
		public void TestDoesNotFindFile()
		{
			string noneExistingFile = @"ThisFileShouldNotExist.exe";
			var results = PathUtilities.LocateFileFromEnviormentPath(noneExistingFile);
			Assert.AreEqual(0,results.Count());
		}
	}
}