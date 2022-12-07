using ApprovalTests.Core;

namespace ApprovalTests.Reporters;

[AttributeUsage(AttributeTargets.All)]
public class UseReporterAttribute : Attribute
{
    public UseReporterAttribute(Type reporter)
    {
        Reporter = LoadReporter(reporter);
    }

    public UseReporterAttribute(params Type[] reporters)
    {
        Reporter = new MultiReporter(reporters.Select(LoadReporter));
    }

    static IApprovalFailureReporter LoadReporter(Type reporter)
    {
        if (!typeof(IApprovalFailureReporter).IsAssignableFrom(reporter))
        {
            return new InvalidReporterConfiguration(reporter);
        }
        return GetSingleton(reporter) ?? CreateInstance(reporter);
    }

    public static IApprovalFailureReporter GetSingleton(Type reporter)
    {
        var singleton = reporter.GetField("INSTANCE");
        if (singleton != null)
        {
            return (IApprovalFailureReporter) singleton.GetValue(null);
        }

        return null;
    }

    public static IApprovalFailureReporter CreateInstance(Type reporter)
    {
        return (IApprovalFailureReporter) Activator.CreateInstance(reporter);
    }

    public IApprovalFailureReporter Reporter { get; set; }
}