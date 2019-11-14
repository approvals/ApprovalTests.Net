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
        public void TestVerifyException()
        {
            using (Namers.ApprovalResults.UniqueForOs())
            {
                Action wrapper = () => throw new Exception();
                var e = ExceptionUtilities.GetException(wrapper);
                Approvals.Verify(e);
            }
        }
    }
}