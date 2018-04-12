using System;
using System.Configuration;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    public class AppConfigReporter : IApprovalFailureReporter
    {
        public static readonly AppConfigReporter INSTANCE = new AppConfigReporter();
        private IApprovalFailureReporter reporter;

        public IApprovalFailureReporter Reporter
        {
            get
            {
                if (reporter == null)
                {
                    reporter = CreateReporter();
                }

                return reporter;
            }
        }


        public void Report(string approved, string received)
        {
            Reporter.Report(approved, received);
        }


        private IApprovalFailureReporter CreateReporter()
        {
            var reporterTypeName = ConfigurationManager.AppSettings["UseReporter"];
            if (string.IsNullOrWhiteSpace(reporterTypeName))
            {
                throw new InvalidOperationException(
                    $@"{GetType()} requires you to configure your app.config/web.config with the following setting:
    <appSettings>
        <add key=""UseReporter"" value=""ApprovalTests.Reporters.DiffReporter, ApprovalTests""/>
    </appSettings>");
            }

            var reporterType = Type.GetType(reporterTypeName, true, ignoreCase: false);
            var instance = Activator.CreateInstance(reporterType);
            return (IApprovalFailureReporter) instance;
        }
    }
}