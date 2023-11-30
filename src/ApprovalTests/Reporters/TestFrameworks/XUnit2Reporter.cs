using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Reporters.TestFrameworks;

public class XUnit2Reporter() :
    AssertReporter(
        "Xunit.Assert, xunit.assert",
        "Equal",
        XUnitStackTraceParser.Attribute)
{
    public readonly static XUnit2Reporter INSTANCE = new();
    static readonly Lazy<bool> isXunit2 = new(IsXunit2);

    static bool IsXunit2()
    {
        return AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Any(_ =>
            {
                var name = _.FullName;
                return name.Contains("xunit.core") ||
                       name.Contains("xunit.assert");
            });
    }

    public override bool IsWorkingInThisEnvironment(string forFile) =>
        base.IsWorkingInThisEnvironment(forFile) && isXunit2.Value;

    protected override void InvokeEqualsMethod(Type type, string[] parameters)
    {
        var method = type.GetMethods()
            .First(_ =>
            {
                var parameterInfos = _.GetParameters();
                return _.Name == areEqual &&
                       parameterInfos.Length == 2 &&
                       parameterInfos.All(_ => _.ParameterType == typeof(string));
            });
        method.Invoke(null, parameters);
    }
}