using ApprovalTests.Reporters.TestFrameworks;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters.Windows
{
    public class WindowsDiffReporter : FirstWorkingReporter
    {
        public static readonly WindowsDiffReporter INSTANCE = new WindowsDiffReporter();

        public WindowsDiffReporter()
            : base(
                // begin-snippet: windows_diff_reporters
                CodeCompareReporter.INSTANCE,
                BeyondCompareReporter.INSTANCE,
                TortoiseDiffReporter.INSTANCE,
                AraxisMergeReporter.INSTANCE,
                P4MergeReporter.INSTANCE,
                WinMergeReporter.INSTANCE,
                KDiffReporter.INSTANCE,
                VisualStudioReporter.INSTANCE,
                FrameworkAssertReporter.INSTANCE,
                QuietReporter.INSTANCE
                // end-snippet
                )
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