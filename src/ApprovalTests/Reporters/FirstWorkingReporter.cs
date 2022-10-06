namespace ApprovalTests.Reporters;

public class FirstWorkingReporter : IEnvironmentAwareReporter, IApprovalReporterWithCleanUp
{
    public IEnumerable<IEnvironmentAwareReporter> Reporters { get; }

    public FirstWorkingReporter(params IEnvironmentAwareReporter[] reporters) :
        this((IEnumerable<IEnvironmentAwareReporter>) reporters)
    {

    }

    public FirstWorkingReporter(IEnumerable<IEnvironmentAwareReporter> reporters)
    {
        Reporters = reporters;
    }

    public void Report(string approved, string received)
    {
        var r = Reporters.FirstOrDefault(x => x.IsWorkingInThisEnvironment(received));
        if (r == null)
        {
            throw new Exception($"{GetType().Name} Could not find a Reporter for file {received}");
        }

        r.Report(approved, received);
    }

    public virtual bool IsWorkingInThisEnvironment(string forFile)
    {
        return Reporters.Any(x => x.IsWorkingInThisEnvironment(forFile));
    }

    public void CleanUp(string approved, string received)
    {
        foreach (var cleanup in Reporters.OfType<IApprovalReporterWithCleanUp>())
        {
            cleanup.CleanUp(approved, received);
        }
    }
}