using System.Linq;
using System.IO;
using ApprovalTests;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.Utilities
{
	[TestClass]
	public class PathUtilitiesTest
	{

        /// <summary>
        /// where.exe does not ship with Windows XP SP3, which some people are still being forced to use. This
        /// causes the <see cref="PathUtilities.LocateFileFromEnviormentPath"/> method to return a null, which 
        /// in turn causes the init of GenericDiffReporter to throw a TypeInitialization exception, followed by a 
        /// TargetInvocation exception. One can download the 'where.exe' from the internet, but it 
        /// would IMO be easier to just fail a test, report the problem, and not break the developer's flow.
        /// In addition, returning a string[] from the  <see cref="PathUtilities.LocateFileFromEnviormentPath"/>
        /// will stop the exception from being thrown, and will allow the developer to continue (provided they've installed
        /// their difftool to a default path). If their tool never loads for them, they'll find themselves inside this source
        /// code pretty quickly, and discover the problem that way. The TypeInit and TypeInvoke exceptions do not give one
        /// a single clue as to what the problem is especially when they are being thrown from inside the DLL from the nuget package.
        /// </summary>
        [TestMethod]
        public void TestDoesWhereExeExist()
        {
            bool doesWhereExist = File.Exists(@"C:\Windows\System32\where.exe") || File.Exists(@"C:\Windows\SysWOW64\where.exe");
            Assert.IsTrue(doesWhereExist, "'where.exe' does not exist, automated searching for your diff tool will not work");
        }


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