using System;
using System.Collections.Generic;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	public class MultiReporter : IApprovalFailureReporter
	{
		private readonly IEnumerable<IApprovalFailureReporter> reporters;

		public MultiReporter(params IApprovalFailureReporter[] reporters)
		{
			this.reporters = reporters;
		}
		public MultiReporter(IEnumerable<IApprovalFailureReporter> reporters)
		{
			this.reporters = reporters;
		}

		public void Report(string approved, string received)
		{
			Exception lastThrown = null;

			foreach (var reporter in reporters)
			{
				try
				{
					reporter.Report(approved, received);

				}
				catch (Exception e)
				{
					lastThrown = e;
				}
			}
			if (lastThrown != null)
			{
				throw lastThrown;
			}
		}
	}
}