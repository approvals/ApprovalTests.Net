#if !__MonoCS__
using System.IO;
using ApprovalTests.Namers;
using ApprovalTests.StackTraceParsers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
	[TestFixture]
	public class VsTestStackTraceNamerTests
	{
		
		[Test]
		public void TestApprovalName()
		{
			string name = new UnitTestFrameworkNamer().Name;
			Assert.AreEqual("VsTestStackTraceNamerTests.TestApprovalName", name);
		}

		[Test]
		public void TestSourcePath()
		{
			string name = new UnitTestFrameworkNamer().SourcePath;
			var path = name.ToLower() + "\\VsTestStackTraceNamerTests.cs";
			Assert.IsTrue(File.Exists(path), path + " does not exist" );
		}

		[Test]
		public void TestMSTestAware()
		{
			Assert.IsTrue(new VSStackTraceParser().IsApplicable());
		}
	}
}
#endif