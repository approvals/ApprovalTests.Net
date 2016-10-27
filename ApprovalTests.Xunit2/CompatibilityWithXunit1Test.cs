using ApprovalTests.Reporters;
using Xunit;

namespace ApprovalTests.Xunit2
{
    public class CompatibilityWithXunit1Test
    {
        [Fact]
        [UseReporter(typeof(FrameworkAssertReporter))]
        public void Xunit2ShouldWrokWithFrameworkAssertReporter()
        {
            Approvals.Verify("this should work");
        }
    }
}