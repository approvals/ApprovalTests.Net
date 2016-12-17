using ApprovalTests.Reporters;
using Xunit;

namespace ApprovalTests.Xunit
{
    public class CompatibilityWithXunit2Tests
    {
        [Fact]
        [UseReporter(typeof(FrameworkAssertReporter))]
        public void XunitShouldBeChosenFromFrameworkAssertReporter()
        {
            Approvals.Verify("this should work");
        }
    }
}