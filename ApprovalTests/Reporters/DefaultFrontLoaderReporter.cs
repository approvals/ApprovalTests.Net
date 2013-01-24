namespace ApprovalTests.Reporters
{
	public class DefaultFrontLoaderReporter : FirstWorkingReporter
	{
		public static readonly DefaultFrontLoaderReporter INSTANCE = new DefaultFrontLoaderReporter();

		public DefaultFrontLoaderReporter():base(TeamCityReporter.INSTANCE, NCrunchReporter.INSTANCE)
		{
			
		}
		 
	}
}