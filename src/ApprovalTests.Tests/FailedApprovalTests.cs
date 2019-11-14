﻿using System;
using ApprovalTests.Core.Exceptions;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    [UseReporter(typeof(CleanupReporter))]
    public class FailedApprovalTests
    {
        private void AssertThrows<T>(TestDelegate code) where T : Exception
        {
            using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
            {
                Assert.Throws<T>(code);
            }
        }

        [Test]
        public void EnumerableDoesNotMatchApproval()
        {
            AssertThrows<ApprovalMismatchException>(() =>
                Approvals.VerifyAll(new[] {"Does not match"}, "collection"));
        }

        [Test]
        public void EnumerableNotApprovedYet()
        {
            AssertThrows<ApprovalMissingException>(() =>
                Approvals.VerifyAll(new[] {"Not approved"}, "collection"));
        }

        [Test]
        public void TextDoesNotMatchApproval()
        {
            AssertThrows<ApprovalMismatchException>(() =>
                Approvals.Verify("should fail with mismatch"));
        }

        [Test]
        public void TextNotApprovedYet()
        {
            AssertThrows<ApprovalMissingException>(() =>
                Approvals.Verify("should fail with a missing exception"));
        }
    }
}