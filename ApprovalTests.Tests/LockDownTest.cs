using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    [UseReporter(typeof (DiffReporter))]
    public class LockDownTests
    {
        public string Echo(params int[] i)
        {
            return StringUtils.ToReadableString(i);
        }

        [Test]
        public void TestLockDown()
        {
            int[] n = {1, 2};
						CombinationApprovals.VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => Echo(a, b, c, d, e, f, g, h, i), n, n, n, n, n, n, n, n, n);
        }
        [Test]
        public void TestLockDown8()
        {
            int[] n = { 1, 2 };
						CombinationApprovals.VerifyAllCombinations((a, b, c, d, e, f, g, h) => Echo(a, b, c, d, e, f, g, h), n, n, n, n, n, n, n, n);
        }
        [Test]
        public void TestLockDown2()
        {
            int[] n = { 1, 2 };
						CombinationApprovals.VerifyAllCombinations((a, b) => Echo(a, b), n, n);
        }
        [Test]
        public void TestExceptions()
        {
			// The devide by zero exception has a different message in Mono.
			using (ApprovalTests.Namers.ApprovalResults.UniqueForOs ()) {
				int[] n = { 0, 2 };
				CombinationApprovals.VerifyAllCombinations ((a, b) => a / b, n, n);
			}   
        }
    }
}