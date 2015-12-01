using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;
using ApprovalTests.Tests.Reporters;
using ApprovalTests.Writers;
using ApprovalUtilities.CallStack;
using NUnit.Framework;

namespace ApprovalTests.Tests.CustomImplementation
{
	[TestFixture]
	public class CustomNamerShouldBeSubstitutableTest
	{
		/// <summary>
		/// Test for Issue #140
		/// https://github.com/approvals/ApprovalTests.Net/issues/140
		/// </summary>
		[Test]
		public void CustomNamerShouldNotDependOnSetCallerTest()
		{
			var approvaltext = "CustomNamerShouldBeSubstitutable";

			var writer = WriterFactory.CreateTextWriter(approvaltext);
			var namer = new CustomNamer();
			var reporter = new MethodLevelReporter();

			Approvals.Verify(writer, namer, reporter);
		}

		private class CustomNamer : IApprovalNamer
		{
			public string Name
			{
				get
				{
					return "CustomNamerShouldBeSubstitutable";
				}
			}

			public string SourcePath
			{
				get
				{
					var stackTraceParser = new StackTraceParser();
					stackTraceParser.Parse(new Caller().StackTrace);
					return stackTraceParser.SourcePath;
				}
			}
		}
	}
}