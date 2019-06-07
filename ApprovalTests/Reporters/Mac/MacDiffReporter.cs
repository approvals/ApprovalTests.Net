using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters.Mac
{
    public class MacDiffReporter : FirstWorkingReporter
    {
        public static readonly MacDiffReporter INSTANCE = new MacDiffReporter();

        public MacDiffReporter()
            : base(
                // begin-snippet: mac_diff_reporters
                BeyondCompareMacReporter.INSTANCE,
                DiffMergeReporter.INSTANCE, 
                KaleidoscopeDiffReporter.INSTANCE,
                ApprovalTests.Reporters.P4MergeReporter.INSTANCE, 
                KDiff3Reporter.INSTANCE,
                TkDiffReporter.INSTANCE, 
                FrameworkAssertReporter.INSTANCE,
                QuietReporter.INSTANCE
                // end-snippet
                )
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            if (OsUtils.IsUnixOs()) return base.IsWorkingInThisEnvironment(forFile);
            return false;
        }
    }
}