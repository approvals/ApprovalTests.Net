using System;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
	[TestFixture]
	public class ExceptionTests
	{
		[Test]
		public void TestVerifyException()
		{
			using (ApprovalTests.Namers.ApprovalResults.UniqueForOs ()) {
				Action wrapper = () => { throw new Exception (); };
				var e = ExceptionUtilities.GetException (wrapper);
				Approvals.Verify (e);
			}
		}
	}
}