using System;
using System.Collections.Generic;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Excutable
{
	[TestFixture]
	[UseReporter(typeof(QuietReporter))]
	public class ExcutableTest
	{
		[Test]
		public void TestExecutableFailure()
		{
			Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
		}

		[Test]
		public void TestExecutableFailureWithPreviousApproval()
		{
			Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
		}
		[Test]
		public void TestExecutableSuccess()
		{
			Approvals.VerifyAll(RunExecutableApproval(), "Increased feedback on");
		}

		private List<string> RunExecutableApproval()
		{
			var output = new List<string>();

			try
			{
				NamerFactory.AdditionalInformation = "Inner";
				Approvals.VerifyWithCallback("Sam", s => output.Add(s));

			}
			catch (Exception)
			{

			}
			return output;
		}
	}
}