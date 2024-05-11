using DiffEngine;

namespace ApprovalTests.Reporters;

public class WinMergeReporter() :
    DiffToolReporter(DiffTool.WinMerge)
{
    public static readonly WinMergeReporter INSTANCE = new();
}