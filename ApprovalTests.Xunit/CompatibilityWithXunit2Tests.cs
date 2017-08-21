using ApprovalTests.Reporters;
using Xunit;
using Xunit.Sdk;

namespace ApprovalTests.Xunit
{
    public class CompatibilityWithXunit2Tests
    {
        [Fact]
        [UseReporter(typeof(FrameworkAssertReporter))]
        public void XunitShouldBeChosenFromFrameworkAssertReporter()
        {
            Assert.Throws<EqualException>(() =>
                Approvals.Verify("this should work"));
        }
    }
}