using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Reporters.TestFrameworks;

public class XUnit2Reporter : AssertReporter
{
    public readonly static XUnit2Reporter INSTANCE = new();
    static readonly Lazy<bool> isXunit2 = new(IsXunit2);

    static bool IsXunit2()
    {
        return AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Any(a =>
            {
                var name = a.FullName;
                return name.Contains("xunit.core") || name.Contains("xunit.assert");
            });
    }

    public XUnit2Reporter()
        : base("Xunit.Assert, xunit.assert", "Equal", XUnitStackTraceParser.Attribute)
    {
    }

    public override bool IsWorkingInThisEnvironment(string forFile) =>
        base.IsWorkingInThisEnvironment(forFile) && isXunit2.Value;

    protected override void InvokeEqualsMethod(Type type, string[] parameters)
    {
        var method = type.GetMethods().First(m => m.Name == areEqual && m.GetParameters().Count() == 2);
        method.Invoke(null, parameters);
    }
}