using System;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests.Async;
using ApprovalTests.Reporters;
using ApprovalTests.Scrubber;
using NUnit.Framework;
#pragma warning disable 162
#pragma warning disable 1998

namespace ApprovalTests.Tests.Async
{
    [TestFixture]
    [UseReporter(typeof(MachineSpecificReporter))]
    public class AsyncTests
    {
        [Test]
        public void TestAsyncExceptionFromVoid()
        {
            using (Namers.ApprovalResults.UniqueForOs())
            {
                AsyncApprovals.VerifyException(ThrowBabyThrow(),
                    ScrubberUtils.Combine(
                        ScrubberUtils.RemoveLinesContaining("System.Linq.Parallel.QueryTask"),
                        ScrubberUtils.RemoveLinesContaining("System.Threading.Tasks.Task.InnerInvoke"))
                );
            }
        }

        private static async Task<int> ThrowBabyThrow()
        {
            return Enumerable.Range(0, 3).AsParallel().WithDegreeOfParallelism(4).Select(i =>
            {
                throw new Exception($"Throwing {i} times");
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

        [Test]
        public async Task TestAsync()
        {
            var text = await AsyncMethod();
            Approvals.Verify(text);
        }

        static async Task<string> AsyncMethod()
        {
            await Task.Delay(1);
            return "This came asynchronously";
        }
    }
}