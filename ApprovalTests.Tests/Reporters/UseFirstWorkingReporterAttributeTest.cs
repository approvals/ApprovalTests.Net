using System;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class UseFirstWorkingReporterAttributeTest
    {
        [Test]
        public void UseFirstWorkingReporterAttribute_requires_environment_aware_reporter()
        {
            var nonEnviroAware = typeof(NotEnvironmentAwareReporter);
            var ex = Assert.Throws<InvalidCastException>(() =>
            {
                new UseFirstWorkingReporterAttribute(nonEnviroAware);
            });
            Assert.That(ex.Message, Is.StringContaining("IEnvironmentAwareReporter"));
        }

        [Test]
        public void UseFirstWorkingReporterAttribute_uses_first_working_reporter()
        {
            var nonWorking = typeof(NonWorkingReporter);
            var alwaysSucceeds = typeof(AlwaysSucceedsReporter);
            var alwaysFails = typeof(AlwaysFailsReporter);
            var attribute = new UseFirstWorkingReporterAttribute(nonWorking, alwaysSucceeds, alwaysFails);
            var reporter = attribute.Reporter;

            Assert.That(((FirstWorkingReporter)reporter).IsWorkingInThisEnvironment(""), Is.True);
            Assert.DoesNotThrow(()=> reporter.Report("", "doesn't match"));
        }

        [Test]
        public void UseFirstWorkingReporterAttribute_requires_all_reporters_to_be_environment_aware()
        {
            var nonEnviroAware1 = typeof(AppConfigReporter);
            var nonEnviroAware2 = typeof(NotEnvironmentAwareReporter);
            var ex = Assert.Throws<InvalidCastException>(()=>
            {
                new UseFirstWorkingReporterAttribute(nonEnviroAware1, nonEnviroAware2);
            });
            Assert.That(ex.Message, Is.StringContaining("IEnvironmentAwareReporter"));
        }
    }

    class AlwaysSucceedsReporter : IEnvironmentAwareReporter
    {
        public void Report(string approved, string received)
        {
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return true;
        }
    }
    class AlwaysFailsReporter : IEnvironmentAwareReporter
    {
        public void Report(string approved, string received)
        {
            throw new Exception("this reporter always fails");
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return true;
        }
    }
    class NonWorkingReporter : IEnvironmentAwareReporter
    {
        public void Report(string approved, string received)
        {
            throw new NotImplementedException();
        }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return false;
        }
    }
    class NotEnvironmentAwareReporter : IApprovalFailureReporter
    {
        public void Report(string approved, string received)
        {
            throw new NotImplementedException();
        }
    }
}

