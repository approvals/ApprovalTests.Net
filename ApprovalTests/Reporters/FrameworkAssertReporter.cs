namespace ApprovalTests.Reporters
{
	public class FrameworkAssertReporter : FirstWorkingReporter
	{
		public static readonly FrameworkAssertReporter INSTANCE = new FrameworkAssertReporter();
		public FrameworkAssertReporter()
			: base(MsTestReporter.INSTANCE,
						 NUnitReporter.INSTANCE,
						 XUnitReporter.INSTANCE)
		{
		}
	}
}