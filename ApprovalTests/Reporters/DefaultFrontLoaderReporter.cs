namespace ApprovalTests.Reporters
{
    public class DefaultFrontLoaderReporter : FirstWorkingReporter
    {
        public static readonly DefaultFrontLoaderReporter INSTANCE = new DefaultFrontLoaderReporter();

        public DefaultFrontLoaderReporter()
            : base(
            TfsReporter.INSTANCE,
            TeamCityReporter.INSTANCE,
            JenkinsReporter.INSTANCE,
            BambooReporter.INSTANCE,
            CruiseControlNetReporter.INSTANCE,
            NCrunchReporter.INSTANCE,
            MightyMooseAutoTestReporter.INSTANCE,
            MyGetReporter.INSTANCE,
            GoContinuousDeliveryReporter.INSTANCE)
        {
        }
    }
}