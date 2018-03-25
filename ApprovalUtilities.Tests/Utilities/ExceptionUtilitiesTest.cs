using System;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalUtilities.Tests.Utilities
{
	public class ExceptionUtilitiesTest
	{
		[Test]
		public void TestGetException()
		{
			AssertException<NotFiniteNumberException>(() => { throw new NotFiniteNumberException(); });
		}
		public static void AssertException<T>(Action action) where T : Exception
		{
			Assert.IsInstanceOf(typeof(T), ExceptionUtilities.GetException(action));
		}
	}
}