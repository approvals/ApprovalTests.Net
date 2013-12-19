namespace ApprovalTests.Reporters
{
	public class ImageReporter : FirstWorkingReporter
	{
		public static readonly ImageReporter INSTANCE = new ImageReporter();

		public ImageReporter() : base(TortoiseImageDiffReporter.INSTANCE,
		                              BeyondCompareReporter.INSTANCE,
            P4MergeImageReporter.INSTANCE)
		{
		}
	}
}