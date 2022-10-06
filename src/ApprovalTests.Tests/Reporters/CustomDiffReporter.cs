public class CustomDiffReporter :
    FirstWorkingReporter
{
    public CustomDiffReporter()
        : base(
            //TODO: re-order or remove as required
            CodeCompareReporter.INSTANCE,
            BeyondCompareReporter.INSTANCE,
            TortoiseDiffReporter.INSTANCE,
            AraxisMergeReporter.INSTANCE,
            P4MergeReporter.INSTANCE,
            WinMergeReporter.INSTANCE,
            KDiff3Reporter.INSTANCE,
            VisualStudioReporter.INSTANCE,
            RiderReporter.INSTANCE,
            FrameworkAssertReporter.INSTANCE,
            QuietReporter.INSTANCE
        )
    {
    }
}