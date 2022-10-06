using DiffEngine;

namespace ApprovalTests.Reporters;

public class P4MergeReporter : DiffToolReporter
{
    public static readonly P4MergeReporter INSTANCE = new();

    public P4MergeReporter() : base(DiffTool.P4Merge)
    {
    }
}