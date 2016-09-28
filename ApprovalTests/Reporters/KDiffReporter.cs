namespace ApprovalTests.Reporters
{

	public class KDiffReporter : GenericDiffReporter
	{
		public static readonly KDiffReporter INSTANCE = new KDiffReporter();
		public KDiffReporter()
            : base(DiffPrograms.Windows.KDIFF3)
		{
		}
	}

}
