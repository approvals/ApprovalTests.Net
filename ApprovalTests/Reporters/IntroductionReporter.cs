using System;
using System.Diagnostics;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
	public class IntroductionReporter : IApprovalFailureReporter
	{
		public static readonly IntroductionReporter INSTANCE = new IntroductionReporter();

		public void Report(string approved, string received)
		{
			var message = GetFriendlyWelcomeMessage();
			Debug.WriteLine(message);
			Console.WriteLine(message);
		}

		public string GetFriendlyWelcomeMessage()
		{
			var message =
				@"Welcome to ApprovalTests.
ApprovalTests use the attribute [UseReporter(typeof(DiffReporter))] on your test class or method.
When you do this ApprovalTest will launch the result using that reporter (for example in your diff tool).
You can find several reporters in ApprovalTests.Reporters namespace, or create your own by extending {0}) interface.
Find more at: http://blog.approvaltests.com/2011/12/using-reporters-in-approval-tests.html

".FormatWith(typeof(IApprovalFailureReporter));
			return message;
		}
	}
}