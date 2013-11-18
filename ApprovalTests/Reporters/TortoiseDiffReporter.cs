namespace ApprovalTests.Reporters
{
	public class TortoiseDiffReporter : FirstWorkingReporter
	{
		public static readonly TortoiseDiffReporter INSTANCE = new TortoiseDiffReporter();

	    public TortoiseDiffReporter(): base(TortoiseTextDiffReporter.INSTANCE,TortoiseComboImageReporter.INSTANCE)
	    {
	        
	    }
	}
}