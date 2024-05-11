namespace ApprovalTests.Reporters;

public class TortoiseImageDiffReporter() :
    DiffToolReporter(DiffTool.TortoiseIDiff)
{
    public static readonly TortoiseImageDiffReporter INSTANCE = new();
}