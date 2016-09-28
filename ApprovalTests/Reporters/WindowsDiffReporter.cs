using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
    public class WindowsDiffReporter : FirstWorkingReporter
    {
        public static readonly WindowsDiffReporter INSTANCE = new WindowsDiffReporter();

        public WindowsDiffReporter()
            : base(
                CodeCompareReporter.INSTANCE,
                BeyondCompareReporter.INSTANCE,
                TortoiseDiffReporter.INSTANCE,
                AraxisMergeReporter.INSTANCE,
                P4MergeReporter.INSTANCE,
                WinMergeReporter.INSTANCE,
                KDiffReporter.INSTANCE,
                VisualStudioReporter.INSTANCE,
                FrameworkAssertReporter.INSTANCE,
                QuietReporter.INSTANCE)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            if (OsUtils.IsWindowsOs())
            {
                return base.IsWorkingInThisEnvironment(forFile);
            }
            return false;
        }
    }
}