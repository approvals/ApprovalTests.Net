
using System.Diagnostics;
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
        var areEquals = type
            .GetMethods(bindingFlags)
            .Single(_ => _.Name=="AreEqual" && _.ContainsGenericParameters && _.GetParameters().Length == 2);
        var method = areEquals.MakeGenericMethod(typeof(string));
        method.Invoke(null, parameters);
    }
}