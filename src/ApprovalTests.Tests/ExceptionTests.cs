using System;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    [UseReporter(typeof(MachineSpecificReporter))]
    public class ExceptionTests
    {
        [Test]
        public void VerifyExceptionWithStacktrace()
        {
            using (Namers.ApprovalResults.UniqueForOs())
            {
                Action wrapper = () => throw new Exception("https://github.com/approvals/ApprovalTests.Net/issues/242");
                var e = ExceptionUtilities.GetException(wrapper);
                Approvals.VerifyExceptionWithStacktrace(e);
            }
        }

        [Test]
        public void VerifyException()
        {
            Action wrapper = () => throw new Exception("The Message");
            var e = ExceptionUtilities.GetException(wrapper);
            Approvals.VerifyException(e);
        }
    }
}