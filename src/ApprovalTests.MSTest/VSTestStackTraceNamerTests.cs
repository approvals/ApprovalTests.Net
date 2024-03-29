[TestClass]
public class VsTestStackTraceNamerTests
{
    [TestMethod]
    public void TestApprovalName()
    {
        var name = new UnitTestFrameworkNamer().Name;
        Assert.AreEqual("VsTestStackTraceNamerTests.TestApprovalName", name);
    }

    [TestMethod]
    public void TestSourcePath()
    {
        var name = new UnitTestFrameworkNamer().SourcePath;
        var path = name.ToLower() + "\\VsTestStackTraceNamerTests.cs";
        Assert.IsTrue(File.Exists(path), path + " does not exist");
    }

    [TestMethod]
    public void TestMSTestAware() =>
        Assert.IsTrue(new VSStackTraceParser().IsApplicable());
}