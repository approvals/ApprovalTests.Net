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
			Action wrapper = () => { throw new Exception(); };
			var e = ExceptionUtilities.GetException(wrapper);
			Approvals.Verify(e);
		}
	}
}