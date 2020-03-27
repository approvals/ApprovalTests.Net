using ApprovalUtilities.Utilities;
using Xunit;

namespace ApprovalUtilities.Tests.Utilities
{
    public class DisposablesTest
    {
        [Fact]
        public void TestDisposable()
        {
            var callCount = 1;
            // begin-snippet: disposables
            using (Disposables.Create(() => callCount++))
            {
                //code
            }
            // end-snippet

            Assert.Equal(2, callCount);

        }

    }
}
