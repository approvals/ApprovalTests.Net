namespace ApprovalTests.Reporters;

public class AraxisMergeReporter() :
    DiffToolReporter(DiffTool.AraxisMerge)
{
    public static readonly AraxisMergeReporter INSTANCE = new();
}