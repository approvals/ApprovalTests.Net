namespace ApprovalTests.Reporters;

public class TortoiseTextDiffReporter() :
    DiffToolReporter(DiffTool.TortoiseMerge)
{
    public static readonly TortoiseTextDiffReporter INSTANCE = new();
}