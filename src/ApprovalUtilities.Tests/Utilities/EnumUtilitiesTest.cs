using ApprovalUtilities.Utilities;
using Xunit;

namespace ApprovalUtilities.Tests.Utilities
{
    public class EnumUtilitiesTest
    {
        [Fact]
        public void TestGetEnumValues()
        {
            var result = EnumUtilities.GetValues<E>();
            Assert.Equal(new[] {E.A, E.B, E.C}, result);
        }

        private enum E
        {
            A,
            B,
            C
        }
    }
}