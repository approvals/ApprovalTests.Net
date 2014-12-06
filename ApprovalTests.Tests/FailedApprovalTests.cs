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
        [ExpectedException(typeof(ApprovalMismatchException))]
        public void EnumerableDoesNotMatchApproval()
        {
            Approvals.VerifyAll(new[] { "Does not match" }, "collection");
        }

        [Test]
        [ExpectedException(typeof(ApprovalMissingException))]
        public void EnumerableNotApprovedYet()
        {
            Approvals.VerifyAll(new[] { "Not approved" }, "collection");
        }

        [Test]
        [ExpectedException(typeof(ApprovalMismatchException))]
        public void TextDoesNotMatchApproval()
        {
            Approvals.Verify("should fail with mismatch");
        }

        [Test]
        [ExpectedException(typeof(ApprovalMissingException))]
        public void TextNotApprovedYet()
        {
            Approvals.Verify("should fail with a missing exception");
        }
    }
}