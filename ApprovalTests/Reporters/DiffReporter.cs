namespace ApprovalTests.Reporters
{
	public class DiffReporter : FirstWorkingReporter
	{
		public static readonly DiffReporter INSTANCE = new DiffReporter();

		public DiffReporter()
			: base(
				CodeCompareReporter.INSTANCE,
				BeyondCompareReporter.INSTANCE,
				TortoiseDiffReporter.INSTANCE,
				AraxisMergeReporter.INSTANCE,
				P4MergeReporter.INSTANCE,
				WinMergeReporter.INSTANCE,
				KDiffReporter.INSTANCE,
#if !__MonoCS__
				VisualStudioReporter.INSTANCE,
#endif
				FrameworkAssertReporter.INSTANCE,
				QuietReporter.INSTANCE)
		{
		}
	}
}