using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests.Scrubber;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Async
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(Approvals),
        Message = @"All modern unit testing frameworks now support async. So Approvals.Verify* should be used after awaiting the task to get the result.
This will yield better performance since other tests can run when a task is being awaited.")]
    public static class AsyncApprovals
    {
        const string exceptionMessage = @"All modern unit testing frameworks now support async. So Approvals.Verify* should be used after awaiting the task to get the exception.
For example in xUnit use Assert.ThrowsAsync to get the exception. Then use Approvals.Verify(Exception).";

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Approvals),
            Message = exceptionMessage)]
        public static void VerifyException(Task task)
        {
            VerifyException(task, ScrubberUtils.NO_SCRUBBER);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Approvals),
            Message = exceptionMessage)]
        public static void VerifyException(Task task, Func<string, string> scrubber)
        {
            var exceptions = new List<Exception>();
            try
            {
                task.Wait();
            }
            catch (AggregateException a)
            {
                var all = a.Flatten().InnerExceptions;
                var sorted = all.OrderBy(e => e.GetType().FullName + "" + e.Message);
                exceptions.AddAll(sorted);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

            Approvals.VerifyAll("Exceptions Thrown", exceptions, e => scrubber(e.Scrub()));
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Approvals),
            Message = exceptionMessage)]
        public static void VerifyException<T>(Func<Task<T>> taskRunner)
        {
            VerifyException(taskRunner(), ScrubberUtils.NO_SCRUBBER);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Approvals),
            Message = exceptionMessage)]
        public static void VerifyException<T>(Func<Task<T>> taskRunner, Func<string, string> scrubber)
        {
            VerifyException(taskRunner(), scrubber);
        }

        public static void Verify<T>(Func<Task<T>> taskRunner)
        {
            var result = taskRunner.Invoke().Result;
            Approvals.Verify(result);
        }
    }
}