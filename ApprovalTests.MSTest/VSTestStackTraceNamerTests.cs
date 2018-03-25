#if !__MonoCS__
using System.IO;
using ApprovalTests.Namers;
using ApprovalTests.StackTraceParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.Tests.Namer
{
	[TestClass]
	public class VsTestStackTraceNamerTests
	{
		
		[TestMethod]
		public void TestApprovalName()
		{
			string name = new UnitTestFrameworkNamer().Name;
			Assert.AreEqual("VsTestStackTraceNamerTests.TestApprovalName", name);
		}

		[TestMethod]
		public void TestSourcePath()
		{
			string name = new UnitTestFrameworkNamer().SourcePath;
			var path = name.ToLower() + "\\VsTestStackTraceNamerTests.cs";
			Assert.IsTrue(File.Exists(path), path + " does not exist" );
		}

		[TestMethod]
		public void TestMSTestAware()
		{
			Assert.IsTrue(new VSStackTraceParser().IsApplicable());
		}
	}
}
#endif