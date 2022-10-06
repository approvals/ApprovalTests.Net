namespace ApprovalTests.Reporters;

public class TortoiseImageDiffReporter : DiffToolReporter
{
    public static readonly TortoiseImageDiffReporter INSTANCE = new();

    public TortoiseImageDiffReporter() : base(DiffTool.TortoiseIDiff)
    {
    }
}