using System.IO;
using ApprovalTests.Namers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
	[TestFixture]
	public class NunitStackTraceNamerTests
	{
		[Test]
		public void TestApprovalName()
		{
			var name = new UnitTestFrameworkNamer().Name;
			Assert.AreEqual("NunitStackTraceNamerTests.TestApprovalName", name);
		}

		[Test]
		public void TestSourcePath()
		{
			var name = new UnitTestFrameworkNamer().SourcePath;
			var path = name.ToLower() + Path.DirectorySeparatorChar + this.GetType().Name + ".cs";
			Assert.IsTrue(File.Exists(path), path + " does not exist");
		}

		[Test()]
		[Description("The approval file should be based on the scenario name with outline")]
		[TestCase("Fred")]
		[TestCase("John")]
		public virtual void TestCaseAttributes(string caseName)
		{
			NamerFactory.AdditionalInformation = caseName;
			var name = new UnitTestFrameworkNamer().Name;
			Assert.AreEqual("NunitStackTraceNamerTests.TestCaseAttributes." + caseName, name);
		}
	}
}