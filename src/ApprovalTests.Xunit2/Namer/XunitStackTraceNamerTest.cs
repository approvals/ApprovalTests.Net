public class XunitStackTraceNamerTest
{
    [Fact]
    public async Task AsyncTestApprovalName()
    {
        var name = new UnitTestFrameworkNamer().Name;
        var sourcePath = new UnitTestFrameworkNamer().SourcePath;

        await AnAsyncMethod();

        Assert.Equal("XunitStackTraceNamerTest.AsyncTestApprovalName", name);
        Assert.True(File.Exists($@"{sourcePath}\XunitStackTraceNamerTest.cs"));
    }

    [Fact]
    public async Task FullAsyncTest()
    {
        await AnAsyncMethod();
        Approvals.Verify("Async");
    }

    [Fact(Skip = "This is Hard")]
    //[Fact]
    public async Task ProperFullAsyncTest()
    {
        await Task.Delay(10);
        // This is the stack trace, and needs to do MAGIC!
        //   at ApprovalTests.Xunit.Namer.XunitStackTraceNamerTest.<ProperFullAsyncTest>d__c.MoveNext()
        Approvals.Verify("Async with Delay");
    }

    [Fact]
    public void TestApprovalName()
    {
        var name = new UnitTestFrameworkNamer().Name;
        Assert.Equal("XunitStackTraceNamerTest.TestApprovalName", name);
    }

    [InheritedFact]
    public void TestApprovalName_InheritedFact()
    {
        var name = new UnitTestFrameworkNamer().Name;
        Assert.Equal("XunitStackTraceNamerTest.TestApprovalName_InheritedFact", name);
    }

    [Fact]
    public void TestApprovalNamerFailureMessage()
    {
        var parser = new StackTraceParser();
        var exception = ExceptionUtilities.GetException(() => parser.Parse(new(6)));

        Approvals.Verify(exception.Message);
    }

    static Task AnAsyncMethod()
    {
        return Task.FromResult(default(object));
    }

    void AssertEquals<T>(string typeName)
    {
        var instance = Type.GetType(typeName, false);
        Assert.Equal(typeof(T), instance);
    }
}

public class InheritedFactAttribute :
    FactAttribute;