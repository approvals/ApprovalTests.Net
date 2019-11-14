using System;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class FrontLoadedReporterAttribute : Attribute
    {
        public FrontLoadedReporterAttribute(Type reporter)
        {
            var instance = UseReporterAttribute.GetSingleton(reporter) ?? UseReporterAttribute.CreateInstance(reporter);
            Reporter = instance as IEnvironmentAwareReporter;
        }
        public IEnvironmentAwareReporter Reporter { get; set; }
    }
}