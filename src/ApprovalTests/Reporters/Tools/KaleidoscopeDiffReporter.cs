namespace ApprovalTests.Reporters;

public class KaleidoscopeDiffReporter : DiffToolReporter
{
    public static readonly KaleidoscopeDiffReporter INSTANCE = new();

    public KaleidoscopeDiffReporter() : base(DiffTool.Kaleidoscope)
    {
    }
}