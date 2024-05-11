namespace ApprovalTests.Reporters.TestFrameworks;

public class NUnit4Reporter : AssertReporter
{
    public readonly static NUnit4Reporter INSTANCE = new();
    static readonly Lazy<bool> isNUnit4 = new(IsisNUnit4);

    static bool IsisNUnit4()
    {
        return AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Any(_ =>
            {
                var name = _.FullName;
                return name.Contains("nunit.framework.legacy");
            });
    }

    public override bool IsWorkingInThisEnvironment(string forFile) =>
        base.IsWorkingInThisEnvironment(forFile) && isNUnit4.Value;

    public NUnit4Reporter()
        : base("NUnit.Framework.Legacy.ClassicAssert, nunit.framework.legacy", "AreEqual", NUnitStackTraceParser.Attribute)
    {
    }
}