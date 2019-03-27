using System;
using ApprovalUtilities.Utilities;
using Xunit;

namespace ApprovalUtilities.Tests.Utilities
{
    public class ExceptionUtilitiesTest
    {
        [Fact]
        public void TestGetException()
        {
            AssertException<NotFiniteNumberException>(() => { throw new NotFiniteNumberException(); });
        }

        private static void AssertException<T>(Action action) where T : Exception
        {
            Assert.IsType<T>(ExceptionUtilities.GetException(action));
        }
    }
}