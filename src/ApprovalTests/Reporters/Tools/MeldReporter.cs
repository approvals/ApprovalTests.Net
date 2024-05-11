namespace ApprovalTests.Reporters;

public class MeldReporter() :
    DiffToolReporter(DiffTool.Meld)
{
    public static readonly MeldReporter INSTANCE = new();
}