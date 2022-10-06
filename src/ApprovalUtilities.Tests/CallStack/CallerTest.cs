using System.Linq;
using ApprovalTests;
using ApprovalUtilities.CallStack;
using Xunit;

public class CallerTest
{
    [Fact]
    public void TestGetCaller()
    {
        var caller = GetCaller();
        Assert.Equal("TestGetCaller", caller.Method.Name);
        Assert.Equal("CallerTest", caller.Class.Name);
    }

    static Caller GetCaller()
    {
        return new Caller();
    }

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

    static Caller GetDeepCaller()
    {
        return A();
    }

    static Caller A()
    {
        return B();
    }

    static Caller B()
    {
        return C();
    }

    static Caller C()
    {
        return D();
    }

    static Caller D()
    {
        return E();
    }

    static Caller E()
    {
        return GetCaller();
    }
}