using System.Reflection;
using ApprovalTests.Namers.StackTraceParsers;
using EmptyFiles;

namespace ApprovalTests.Reporters.TestFrameworks;

public class AssertReporter(string assertClass, string areEqual, string frameworkAttribute)
    : IEnvironmentAwareReporter
{
    protected readonly string areEqual = areEqual;

    public virtual void Report(string approved, string received) =>
        AssertFileContents(approved, received);

    public virtual bool IsWorkingInThisEnvironment(string forFile) =>
        FileExtensions.IsTextFile(forFile) && IsFrameworkUsed();

    public bool IsFrameworkUsed() =>
        AttributeStackTraceParser.GetFirstFrameForAttribute(Approvals.CurrentCaller, frameworkAttribute) !=
        null;

    public void AssertFileContents(string approved, string received)
    {
        var a = File.Exists(approved) ? File.ReadAllText(approved) : "";
        var r = File.ReadAllText(received);
        QuietReporter.DisplayCommandLineApproval(approved, received);

        AssertEqual(a, r);
    }

    public void AssertEqual(string approvedContent, string receivedContent)
    {
        try
        {
            var type = Type.GetType(assertClass);
            var parameters = new[] { approvedContent, receivedContent };
            InvokeEqualsMethod(type, parameters);
        }
        catch (TargetInvocationException e)
        {
            throw e.GetBaseException();
        }
    }

    protected virtual void InvokeEqualsMethod(Type type, string[] parameters)
    {
        var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
        type.InvokeMember(areEqual, bindingFlags, null, null, parameters);
    }
}