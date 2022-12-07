
using System.Reflection;
using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Reporters.TestFrameworks;

public class MsTestReporter : AssertReporter
{
    public readonly static MsTestReporter INSTANCE = new();

    public MsTestReporter()
        : base(
            assertClass: "Microsoft.VisualStudio.TestTools.UnitTesting.Assert, Microsoft.VisualStudio.TestPlatform.TestFramework",
            areEqual: "AreEqual",
            frameworkAttribute: VSStackTraceParser.Attribute)
    {
    }

    protected override void InvokeEqualsMethod(Type type, string[] parameters)
    {
        var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
        var method = type.GetMethod("AreEqual", bindingFlags)!.MakeGenericMethod(typeof(string));
        method.Invoke(null, parameters);
    }
}