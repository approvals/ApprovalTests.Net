namespace ApprovalTests.Reporters;

public class P4MergeReporter() :
    DiffToolReporter(DiffTool.P4Merge)
{
    public static readonly P4MergeReporter INSTANCE = new();
}