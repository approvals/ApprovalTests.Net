using System.Linq;
using ApprovalTests;
using ApprovalUtilities.CallStack;
using Xunit;

namespace ApprovalUtilities.Tests.CallStack;

public class CallerTest
{
    [Fact]
    public void TestGetCaller()
    {
        var caller = GetCaller();
        Assert.Equal("TestGetCaller", caller.Method.Name);
        Assert.Equal("CallerTest", caller.Class.Name);
    }

    private Caller GetCaller()
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

    private Caller GetDeepCaller()
    {
        return A();
    }

    private Caller A()
    {
        return B();
    }

    private Caller B()
    {
        return C();
    }

    private Caller C()
    {
        return D();
    }

    private Caller D()
    {
        return E();
    }

    private Caller E()
    {
        return GetCaller();
    }
}