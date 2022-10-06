namespace ApprovalTests.Reporters;

public class TkDiffReporter : DiffToolReporter
{
    public static readonly TkDiffReporter INSTANCE = new();

    public TkDiffReporter() : base(DiffTool.TkDiff)
    {
    }
}