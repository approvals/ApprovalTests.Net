namespace ApprovalTests.Reporters;

public class TortoiseGitTextDiffReporter() :
    DiffToolReporter(DiffTool.TortoiseGitMerge)
{
    public static readonly TortoiseGitTextDiffReporter INSTANCE = new();
}