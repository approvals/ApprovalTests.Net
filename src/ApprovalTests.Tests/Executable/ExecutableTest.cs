using System;
using System.Collections.Generic;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Executable
{
    [TestFixture]
    [UseReporter(typeof(QuietReporter))]
    public class ExecutableTest
    {
        private List<string> RunExecutableApproval()
        {
            var output = new List<string>();

            try
            {
                NamerFactory.AdditionalInformation = "Inner";
                Approvals.VerifyWithCallback("Sam", s => output.Add(s));
            }
            catch (Exception)
            {
            }

            return output;
        }

        [Test]
        public void TestExecutableFailure()
        {
            using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
            {
                Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
            }
        }

        [Test]
        public void TestExecutableFailureWithPreviousApproval()
        {
            using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
            {
                Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
            }
        }

        [Test]
        public void TestExecutableSuccess()
        {
            using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
            {
                Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
            }
        }
    }
}