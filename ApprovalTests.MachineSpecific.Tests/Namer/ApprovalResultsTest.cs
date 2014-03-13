using System;
using ApprovalTests.Namers;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MachineSpecific.Tests.Namer
{
	[TestClass]
	public class ApprovalResultsTest
	{
		[TestMethod]
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