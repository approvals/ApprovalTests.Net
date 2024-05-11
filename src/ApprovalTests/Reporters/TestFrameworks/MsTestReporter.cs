namespace ApprovalTests.Reporters.TestFrameworks;

public class MsTestReporter() :
    AssertReporter(
        assertClass: "Microsoft.VisualStudio.TestTools.UnitTesting.Assert, Microsoft.VisualStudio.TestPlatform.TestFramework",
        areEqual: "AreEqual",
        frameworkAttribute: VSStackTraceParser.Attribute)
{
    public readonly static MsTestReporter INSTANCE = new();

    protected override void InvokeEqualsMethod(Type type, string[] parameters)
    {
        var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
        var areEquals = type
            .GetMethods(bindingFlags)
            .Single(_ => _.Name == "AreEqual" &&
                         _.ContainsGenericParameters &&
                         _.GetParameters().Length == 2);
        var method = areEquals.MakeGenericMethod(typeof(string));
        method.Invoke(null, parameters);
    }
}