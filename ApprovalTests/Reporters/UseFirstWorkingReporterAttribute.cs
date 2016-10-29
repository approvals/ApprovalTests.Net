using System;
using System.ComponentModel;
using System.Linq;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    [AttributeUsage(AttributeTargets.All)]
    [Description("Use the first reporter that works in the current environment")]
    public class UseFirstWorkingReporterAttribute : UseReporterAttribute
    {
        public UseFirstWorkingReporterAttribute(Type reporterType)
        {
            var reporter = (IEnvironmentAwareReporter) Activator.CreateInstance(reporterType);
            SetReporter(reporter);
        }

        //This constructor keeps the Attribute CLS-compliant for two parameters.
        //Using more than two constructor parameters will use the params constructor which is not CLS-compliant
        public UseFirstWorkingReporterAttribute(Type reporterType, Type reporterType2)
        {
            var reporter = (IEnvironmentAwareReporter) Activator.CreateInstance(reporterType);
            var reporter2 = (IEnvironmentAwareReporter)Activator.CreateInstance(reporterType2);
            SetReporter(reporter, reporter2);
        }

        public UseFirstWorkingReporterAttribute(params Type[] reporterTypes)
        {
            var reporters = reporterTypes
                .Select(reporterType => (IEnvironmentAwareReporter) Activator.CreateInstance(reporterType) )
                .ToArray();
            SetReporter(reporters);
        }

        private void SetReporter(params IEnvironmentAwareReporter[] reporters)
        {
            Reporter = new FirstWorkingReporter(reporters);
        }
    }
}
