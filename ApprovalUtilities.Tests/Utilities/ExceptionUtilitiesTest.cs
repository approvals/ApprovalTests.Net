using System;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.Utilities
{
	[TestClass]
	public class ExceptionUtilitiesTest
	{
		[TestMethod]
		public void TestGetException()
		{
			AssertException<NotFiniteNumberException>(() => { throw new NotFiniteNumberException(); });
		}
		public static void AssertException<T>(Action action) where T : Exception
		{
			Assert.IsInstanceOfType(ExceptionUtilities.GetException(action), typeof(T));
		}
	}
}