[TestFixture]
public class NunitStackTraceNamerTests
{
    [Test]
    public void TestApprovalName()
    {
        var name = new UnitTestFrameworkNamer().Name;
        ClassicAssert.AreEqual("NunitStackTraceNamerTests.TestApprovalName", name);
    }

    [Test]
    public void TestSourcePath()
    {
        var path = Approvals.GetDefaultNamer().SourcePath;
        ClassicAssert.IsNotEmpty(path);
        var fullPath = path.ToLower() + Path.DirectorySeparatorChar + GetType().Name + ".cs";
        ClassicAssert.IsTrue(File.Exists(fullPath), fullPath + " does not exist");
    }

    [Test]
    [Description("The approval file should be based on the scenario name with outline")]
    [TestCase("Fred")]
    [TestCase("John")]
    public void TestCaseAttributes(string caseName)
    {
        NamerFactory.AdditionalInformation = caseName;
        var name = new UnitTestFrameworkNamer().Name;
        ClassicAssert.AreEqual("NunitStackTraceNamerTests.TestCaseAttributes." + caseName, name);
    }
}