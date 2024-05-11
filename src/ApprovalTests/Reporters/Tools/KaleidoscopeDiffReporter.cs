namespace ApprovalTests.Reporters;

public class KaleidoscopeDiffReporter() :
    DiffToolReporter(DiffTool.Kaleidoscope)
{
    public static readonly KaleidoscopeDiffReporter INSTANCE = new();
}