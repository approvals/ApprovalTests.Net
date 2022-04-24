using DiffEngine;

namespace ApprovalTests.Reporters;

public class TortoiseGitTextDiffReporter : DiffToolReporter
{
    public static readonly TortoiseGitTextDiffReporter INSTANCE = new();

    public TortoiseGitTextDiffReporter() : base(DiffTool.TortoiseGitMerge)
    {
    }
}