namespace ApprovalTests.Reporters;

public class CodeCompareReporter : DiffToolReporter
{
    public static readonly CodeCompareReporter INSTANCE = new();

    public CodeCompareReporter() : base(DiffTool.CodeCompare)
    {
    }
}