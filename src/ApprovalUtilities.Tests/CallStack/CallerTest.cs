using ApprovalUtilities.CallStack;

public class CallerTest
{
    [Fact]
    public void TestGetCaller()
    {
        var caller = GetCaller();
        Assert.Equal("TestGetCaller", caller.Method.Name);
        Assert.Equal("CallerTest", caller.Class.Name);
    }

    static Caller GetCaller() => new();

    [Fact]
    public void TestCallStack()
    {
        var caller = GetDeepCaller();
        var callers = caller.Callers.Where(c => c.Method.DeclaringType == GetType());
        Approvals.VerifyAll(callers, c => c.Method.ToStandardString());
    }

    [Fact]
    public void TestName()
    {
        var caller = GetDeepCaller();
        var methods = caller.Methods.Where(m => m.DeclaringType == GetType());
        Approvals.VerifyAll(methods, m => m.ToStandardString());
    }

    static Caller GetDeepCaller() => A();

    static Caller A() => B();

    static Caller B() => C();

    static Caller C() => D();

    static Caller D() => E();

    static Caller E() => GetCaller();
}