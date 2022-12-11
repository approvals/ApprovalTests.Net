public class PathUtilitiesTest
{
    [Fact]
    public void ScrubPathTest()
    {
        var dir = PathUtilities.GetDirectoryForCaller();
        var file = dir + "PathUtilitiesTest.cs";
        AssertEqualIgnoreCase(@"...\PathUtilitiesTest.cs", file.ScrubPath(dir));
    }

    [Fact]
    public void TestFindsFile()
    {
        var found = PathUtilities.LocateFileFromEnvironmentPath("ipconfig.exe").FirstOrDefault();
        AssertEqualIgnoreCase(@"C:\Windows\System32\ipconfig.exe", found);
    }

    static void AssertEqualIgnoreCase(string expected, string actual) =>
        Assert.Equal(expected.ToLowerInvariant(), actual.ToLowerInvariant());

    [Fact]
    public void TestFindsMultipleFiles() =>
        Approvals.VerifyAll(PathUtilities.LocateFileFromEnvironmentPath("notepad.exe").Where(x=>!x.Contains("AppData")).Select(f => f.ToLowerInvariant()), "Found");

    [Fact]
    public void TestDoesNotFindFile()
    {
        var noneExistingFile = "ThisFileShouldNotExist.exe";
        var results = PathUtilities.LocateFileFromEnvironmentPath(noneExistingFile);
        Assert.Empty(results);
    }
}