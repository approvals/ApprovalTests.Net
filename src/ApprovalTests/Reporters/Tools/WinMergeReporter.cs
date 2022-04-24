using DiffEngine;

namespace ApprovalTests.Reporters;

public class WinMergeReporter : DiffToolReporter
{
    public static readonly WinMergeReporter INSTANCE = new WinMergeReporter();

    public WinMergeReporter() : base(DiffTool.WinMerge)
    {
    }
}