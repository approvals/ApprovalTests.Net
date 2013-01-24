using System;
using System.Collections.Generic;
using ApprovalTests.Core;
using System.Linq;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class FirstWorkingReporter:IEnvironmentAwareReporter
	{
		private readonly IEnumerable<IEnvironmentAwareReporter> reporters;
		public FirstWorkingReporter(params IEnvironmentAwareReporter[] reporters):
			this((IEnumerable<IEnvironmentAwareReporter>)reporters)
		{
			
		}

		public FirstWorkingReporter(IEnumerable<IEnvironmentAwareReporter> reporters)
		{
			this.reporters = reporters;
		}
		public void Report(string approved, string received)
		{
			var r = reporters.FirstOrDefault(x => x.IsWorkingInThisEnvironment(received));
			if (r == null)
			{
				throw new Exception("{0} Could not find a Reporter for file {1}".FormatWith(GetType().Name, received));
			}
			r.Report(approved, received);
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return reporters.Any(x => x.IsWorkingInThisEnvironment(forFile));
		}
	}
}