namespace ApprovalTests.Reporters;

public class RiderReporter : DiffToolReporter
{
    public static readonly RiderReporter INSTANCE = new();

    public RiderReporter() : base(DiffTool.Rider)
    {
    }
}