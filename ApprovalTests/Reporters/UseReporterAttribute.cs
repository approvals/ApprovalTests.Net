using System;
using System.Linq;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
	[AttributeUsage(AttributeTargets.All)]
	public class UseReporterAttribute : Attribute
	{
		public UseReporterAttribute(Type reporter)
		{
			Reporter = GetSingleton(reporter) ?? CreateInstance(reporter);

		}
		public static IApprovalFailureReporter GetSingleton(Type reporter)
		{
			var singleton = reporter.GetField("INSTANCE");
			if (singleton != null)
			{
				return (IApprovalFailureReporter)singleton.GetValue(null);
			}
			return null;
		}

	    public static IApprovalFailureReporter CreateInstance(Type reporter)
		{
			return (IApprovalFailureReporter)Activator.CreateInstance(reporter);
		}

		public UseReporterAttribute(params Type[] reporters)
		{
			Reporter = new MultiReporter(reporters.Select(r => (IApprovalFailureReporter)Activator.CreateInstance(r)));
		}

		public IApprovalFailureReporter Reporter { get; set; }
	}
}