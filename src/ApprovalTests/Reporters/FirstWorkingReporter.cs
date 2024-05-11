namespace ApprovalTests.Reporters;

public class FirstWorkingReporter(IEnumerable<IEnvironmentAwareReporter> reporters) :
    IEnvironmentAwareReporter, IApprovalReporterWithCleanUp
{
    public IEnumerable<IEnvironmentAwareReporter> Reporters { get; } = reporters;

    public FirstWorkingReporter(params IEnvironmentAwareReporter[] reporters) :
        this((IEnumerable<IEnvironmentAwareReporter>) reporters)
    {

    }

    public void Report(string approved, string received)
    {
        var r = Reporters.FirstOrDefault(_ => _.IsWorkingInThisEnvironment(received));
        if (r == null)
        {
            throw new($"{GetType().Name} Could not find a Reporter for file {received}");
        }

        r.Report(approved, received);
    }

    public virtual bool IsWorkingInThisEnvironment(string forFile)
    {
        return Reporters.Any(_ => _.IsWorkingInThisEnvironment(forFile));
    }

    public void CleanUp(string approved, string received)
    {
        foreach (var cleanup in Reporters.OfType<IApprovalReporterWithCleanUp>())
        {
            cleanup.CleanUp(approved, received);
        }
    }
}