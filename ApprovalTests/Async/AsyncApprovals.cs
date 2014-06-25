using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests.Scrubber;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Async
{
	public class AsyncApprovals
	{
		public static void VerifyException(Task task)
		{
			VerifyException(task, ScrubberUtils.NO_SCRUBBER);
		}

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

		public static void VerifyException<T>(Func<Task<T>> taskRunner)
		{
			VerifyException(taskRunner(), ScrubberUtils.NO_SCRUBBER);
		}

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