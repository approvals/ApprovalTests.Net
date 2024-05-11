namespace ApprovalTests.Reporters;

public class TkDiffReporter() :
    DiffToolReporter(DiffTool.TkDiff)
{
    public static readonly TkDiffReporter INSTANCE = new();
}