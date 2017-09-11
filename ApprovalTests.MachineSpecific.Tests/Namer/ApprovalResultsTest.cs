using System;
using ApprovalTests.Namers;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.MachineSpecific.Tests.Namer
{
	[TestFixture]
	public class ApprovalResultsTest
	{
		[Test]
		public void TestUniqueNames()
		{
			ApprovalResults.UniqueForMachineName();
			var methods = new Func<string>[]
				{
					ApprovalResults.GetDotNetVersion,
					ApprovalResults.GetOsName,
					ApprovalResults.GetUserName
				};
			Approvals.VerifyAll(
				methods,
				m => "{0} => {1}".FormatWith(m.Method.Name, m.Invoke()));
		}
	}
}