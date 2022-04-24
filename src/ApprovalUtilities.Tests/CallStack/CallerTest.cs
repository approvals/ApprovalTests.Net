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

    Caller GetCaller()
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

    Caller GetDeepCaller()
    {
        return A();
    }

    Caller A()
    {
        return B();
    }

    Caller B()
    {
        return C();
    }

    Caller C()
    {
        return D();
    }

    Caller D()
    {
        return E();
    }

    Caller E()
    {
        return GetCaller();
    }
}