using System;
using ApprovalTests.ExceptionalExceptions;
using NUnit.Framework;

namespace ApprovalTests.Tests.ExceptionalExceptions
{
	[TestFixture]
	public class ExceptionExceptionsTest
	{
		[Test]
		public void TestUniqueId()
		{
			var exceptionalId = Exceptional.GenerateUniqueId<Exception>();
			Approvals.Verify(exceptionalId);
		}


		//[Test]
		public void TestTlDr()
		{
			Approvals.Verify(new ExceptionalTlDr(new ExceptionalId
			{
				Assembly = "Assembly",
				Class = "Class",
				Exception = "Exception",
				Method = "Method",
			}));
		}
	}
}