using ApprovalTests.Core.Exceptions;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    [UseReporter(typeof(CleanupReporter))]
    public class FailedApprovalTests
    {
        [Test]
        public void EnumerableDoesNotMatchApproval()
        {
            Assert.Throws<ApprovalMismatchException>(() =>
                Approvals.VerifyAll(new[] {"Does not match"}, "collection"));
        }

        [Test]
        public void EnumerableNotApprovedYet()
        {
            Assert.Throws<ApprovalMissingException>(() =>
                Approvals.VerifyAll(new[] {"Not approved"}, "collection"));
        }

        [Test]
        public void TextDoesNotMatchApproval()
        {
            Assert.Throws<ApprovalMismatchException>(() =>
                Approvals.Verify("should fail with mismatch"));
        }

        [Test]
        public void TextNotApprovedYet()
        {
            Assert.Throws<ApprovalMissingException>(() =>
                Approvals.Verify("should fail with a missing exception"));
        }
    }
}