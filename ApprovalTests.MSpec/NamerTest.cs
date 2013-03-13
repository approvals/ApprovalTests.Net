using System.Diagnostics;
using ApprovalTests.Reporters;
using ApprovalTests.StackTraceParsers;
using Machine.Specifications;

namespace ApprovalTests.MSpec
{
	[UseReporter(typeof(DiffReporter))]
	public class NamerTest
	{
		private Establish start = () => { };
		private Because of = () => { };

		private It should = () =>
			{
				var namer = new MSpecStackTraceParser();
				namer.Parse(new StackTrace());
				namer.ApprovalName.ShouldEqual("NamerTest.should");
			};
	
		private It shouldWork = () => { Approvals.Verify("Hello MSpec!"); };
	}
}