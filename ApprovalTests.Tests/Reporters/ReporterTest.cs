using System;
using ApprovalTests.Core;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.ContinuousIntegration;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class ReporterTest
    {
        [Test]
        public void Testname()
        {
            var old = Environment.GetEnvironmentVariable(NCrunchReporter.EnvironmentVariable);
            Environment.SetEnvironmentVariable(NCrunchReporter.EnvironmentVariable, "1");
            Assert.IsTrue(NCrunchReporter.INSTANCE.IsWorkingInThisEnvironment("a.txt"));
            Environment.SetEnvironmentVariable(NCrunchReporter.EnvironmentVariable, old);
        }

        [Test]
        public void TestInvalidReporterShouldThrow()
        {
            var attribute = new UseReporterAttribute(typeof(ReporterTest));
            VerifyReporterAttribute(attribute);
        }

        [Test]
        public void TestMultipleWithInvalidReporterShouldThrow()
        {
            var attribute = new UseReporterAttribute(typeof(ReporterTest), typeof(string));
            VerifyReporterAttribute(attribute);
        }

        [Test]
        public void TestMachineSpecificName()
        {
            var approvalsFilename = ApprovalsFilename.Parse(@"C:\Users\olgica\Documents\GitHub\ApprovalTests.Net\ApprovalTests.Tests\Email\EmailTest.Testname.Microsoft_Windows_10_Education.approved.eml");
            Approvals.Verify(approvalsFilename);
            Assert.True(approvalsFilename.IsMachineSpecific);
        }

        [Test]
        public void TestNonMachineSpecificName()
        {
            Approvals.Verify(ApprovalsFilename.Parse(@"C:\Users\olgica\Documents\GitHub\ApprovalTests.Net\ApprovalTests.Tests\Email\EmailTest.Testname.approved.eml"));
        }

        private static void VerifyReporterAttribute(UseReporterAttribute attribute)
        {
            var reporter = (IEnvironmentAwareReporter) attribute.Reporter;
            var reportException = Assert.Throws<Exception>(() => reporter.Report("a.txt", "a.txt"));
            var isWorkingException = Assert.Throws<Exception>(() => reporter.IsWorkingInThisEnvironment("a.txt"));

            Approvals.Verify($@"{reportException.Message}

{isWorkingException.Message}");
        }
    }


}