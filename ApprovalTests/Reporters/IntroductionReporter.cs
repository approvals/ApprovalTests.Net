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
            throw new Exception(message);
		}

		public string GetFriendlyWelcomeMessage()
		{
			var message =
                @"Welcome to ApprovalTests.

Please add:

[UseReporter(typeof(DiffReporter))]
to your Class, Method or assembly.

Why:
ApprovalTests uses the [UseReporter] attribute from your test class, method or assembly.
When you do this ApprovalTest will launch the result using that reporter (for example in your diff tool).
You can find several reporters in ApprovalTests.Reporters namespace, or create your own by extending {0}) interface.
Find more at: http://blog.approvaltests.com/2011/12/using-reporters-in-approval-tests.html

".FormatWith(typeof(IApprovalFailureReporter));
			return message;
		}
	}
}