using ApprovalTests.Reporters.Mac;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
    public class MacDiffReporter : FirstWorkingReporter
    {
        public static readonly MacDiffReporter INSTANCE = new MacDiffReporter();

        public MacDiffReporter()
            : base(
                BeyondCompareMacReporter.INSTANCE, DiffMergeReporter.INSTANCE, KaleidoscopeDiffReporter.INSTANCE,
                P4MergeReporter.INSTANCE, KDiff3Reporter.INSTANCE, TkDiffReporter.INSTANCE, FrameworkAssertReporter.INSTANCE,
                QuietReporter.INSTANCE)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            if (OsUtils.IsUnixOs())
            {
                return base.IsWorkingInThisEnvironment(forFile);
            }
            return false;
        }

    }
}