using System;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests {
#if DEBUG
    [UseReporter(typeof(BeyondCompareReporter))]
#else
    [UseReporter(typeof(NUnitReporter))]
#endif
    [TestFixture]
    public class InMemoryStringApproverTests
    {
        [Test]
        public void TestSuccessVerifyFilePath()
        {
            Approvals.VerifyFile(@"..\..\MyFile.error", "My Exception Content");
        }
    }
}