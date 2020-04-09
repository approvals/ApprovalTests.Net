namespace ApprovalTests.Reporters.Linux
{
    public class LinuxDiffReporter : FirstWorkingReporter
    {
        public static readonly LinuxDiffReporter INSTANCE = new LinuxDiffReporter();

        public LinuxDiffReporter()
            : base(
                // begin-snippet: linux_diff_reporters
                DiffMergeReporter.INSTANCE,
                MeldReporter.INSTANCE
                // end-snippet
            )
        {
        }
    }
}