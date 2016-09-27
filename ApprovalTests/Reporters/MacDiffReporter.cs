using ApprovalTests.Reporters.Mac;

namespace ApprovalTests.Reporters
{
    public class MacDiffReporter : FirstWorkingReporter
    {
        public static readonly MacDiffReporter INSTANCE = new MacDiffReporter();

        public MacDiffReporter()
            : base(
                BeyondCompareMacReporter.INSTANCE, DiffMergeReporter.INSTANCE, KaleidoscopeDiffReporter.INSTANCE,
                P4MergeReporter.INSTANCE, KDiff3Reporter.INSTANCE, TkDiffReporter.INSTANCE)

        {
        }
    }
}