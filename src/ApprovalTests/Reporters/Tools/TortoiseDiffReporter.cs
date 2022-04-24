namespace ApprovalTests.Reporters;

public class TortoiseDiffReporter : FirstWorkingReporter
{
    public static readonly TortoiseDiffReporter INSTANCE = new();

    public TortoiseDiffReporter() : base(TortoiseTextDiffReporter.INSTANCE, TortoiseGitTextDiffReporter.INSTANCE, TortoiseImageDiffReporter.INSTANCE)
    {
    }
}