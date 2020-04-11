using ApprovalTests.Reporters.ContinuousIntegration;

namespace ApprovalTests.Reporters
{
    public class DefaultFrontLoaderReporter : FirstWorkingReporter
    {
        public static readonly DefaultFrontLoaderReporter INSTANCE = new DefaultFrontLoaderReporter();

        public DefaultFrontLoaderReporter()
            : base(
            // begin-snippet: continuous_integration
            TfsReporter.INSTANCE,
            TfsVnextReporter.INSTANCE,
            TeamCityReporter.INSTANCE,
            JenkinsReporter.INSTANCE,
            BambooReporter.INSTANCE,
            NCrunchReporter.INSTANCE,
            MightyMooseAutoTestReporter.INSTANCE,
            MyGetReporter.INSTANCE,
            GoContinuousDeliveryReporter.INSTANCE,
            AppVeyorReporter.INSTANCE
            // end-snippet
            )
        {
        }
    }
}