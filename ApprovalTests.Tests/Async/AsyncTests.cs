using System;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests.Async;
using ApprovalTests.Scrubber;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Async
{
	[TestFixture]
	public class AsyncTests
	{
		[Test]
		public void TestAsyncExceptionFromVoid()
		{
			using (ApprovalTests.Namers.ApprovalResults.UniqueForOs ()) {
				AsyncApprovals.VerifyException (ThrowBabyThrow (),
					ScrubberUtils.RemoveLinesContaining ("System.Linq.Parallel.QueryTask"));
			}
		}

		private static async Task<int> ThrowBabyThrow()
		{
			return Enumerable.Range(0, 3).AsParallel().WithDegreeOfParallelism(4).Select(i =>
			{
				throw new Exception("Throwing {0} times".FormatWith(i));
				return 1;
			}).Sum();
		}

		[Test]
		public void TestNoExceptions()
		{
			AsyncApprovals.VerifyException(async () => 1);
		}
		[Test]
		public void TestVerify()
		{
			AsyncApprovals.Verify(async () => "This came asynchronously");
		}
	}
}