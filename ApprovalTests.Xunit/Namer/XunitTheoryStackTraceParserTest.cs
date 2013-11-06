using ApprovalTests.Namers;
using Xunit;
using Xunit.Extensions;

namespace ApprovalTests.Xunit.Namer
{
	public class XunitTheoryStackTraceParserTest
	{
		[Theory]
		[InlineData]
		public void TestApprovalName()
		{
			var name = new UnitTestFrameworkNamer().Name;
			Assert.Equal("XunitTheoryStackTraceParserTest.TestApprovalName", name);
		}
 
		[Theory]
		[InlineData("file1.txt")]
		[InlineData("file2.txt")]
		public void TestApprovalNameWithAdditionalInformation(string fileName)
		{
			NamerFactory.AdditionalInformation = fileName;
			var name = new UnitTestFrameworkNamer().Name;
			Assert.Equal("XunitTheoryStackTraceParserTest.TestApprovalNameWithAdditionalInformation." + fileName, name);
		}
	}
}