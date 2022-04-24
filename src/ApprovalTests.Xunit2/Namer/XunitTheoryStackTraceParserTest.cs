#pragma warning disable xUnit1006 // Theory methods should have parameters
namespace ApprovalTests.Xunit2.Namer;

using Namers;

using Xunit;

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
        using (ApprovalResults.ForScenario(fileName))
        {
            var name = Approvals.GetDefaultNamer().Name;
            Assert.Equal("XunitTheoryStackTraceParserTest.TestApprovalNameWithAdditionalInformation.ForScenario." + fileName,
                name);
        }
    }

    [Theory]
    [InlineData("File \\;:/\"1.txt")]
    public void TestInvalidCharacters(string fileName)
    {
        using (ApprovalResults.ForScenario(fileName))
        {
            var name = new UnitTestFrameworkNamer().Name;
            Assert.Equal("XunitTheoryStackTraceParserTest.TestInvalidCharacters.ForScenario.File _;___1.txt", name);
        }
    }
}