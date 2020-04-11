namespace ApprovalTests.Reporters.Windows
{
    public class TortoiseDiffReporter : FirstWorkingReporter
    {
        public static readonly TortoiseDiffReporter INSTANCE = new TortoiseDiffReporter();

        public TortoiseDiffReporter() : base(TortoiseTextDiffReporter.INSTANCE, TortoiseGitTextDiffReporter.INSTANCE, TortoiseImageDiffReporter.INSTANCE)
        {
        }
    }
}