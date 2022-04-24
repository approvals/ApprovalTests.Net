using DiffEngine;

namespace ApprovalTests.Reporters;

public class TortoiseTextDiffReporter : DiffToolReporter
{
    public static readonly TortoiseTextDiffReporter INSTANCE = new();

    public TortoiseTextDiffReporter() : base(DiffTool.TortoiseMerge)
    {
    }
}