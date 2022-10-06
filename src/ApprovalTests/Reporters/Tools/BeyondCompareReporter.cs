namespace ApprovalTests.Reporters;

public class BeyondCompareReporter : DiffToolReporter
{
    public static readonly BeyondCompareReporter INSTANCE = new();

    public BeyondCompareReporter()
        : base(DiffTool.BeyondCompare)
    {
    }
}