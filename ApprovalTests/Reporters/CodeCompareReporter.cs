namespace ApprovalTests.Reporters
{
	public class CodeCompareReporter : GenericDiffReporter
	{
		public static readonly CodeCompareReporter INSTANCE = new CodeCompareReporter();

		public CodeCompareReporter()
			: base(DiffPrograms.Windows.CODE_COMPARE)
		{
		}
	}
}