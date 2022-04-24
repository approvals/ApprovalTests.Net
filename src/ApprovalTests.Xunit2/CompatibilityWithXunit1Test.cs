using ApprovalTests.Reporters;
using ApprovalTests.Reporters.TestFrameworks;
using Xunit;
using Xunit.Sdk;

namespace ApprovalTests.Xunit2;

public class CompatibilityWithXunit1Test
{
    [Fact]
    [UseReporter(typeof(FrameworkAssertReporter))]
    public void Xunit2ShouldWorkWithFrameworkAssertReporter()
    {
        Assert.Throws<EqualException>(() =>
            Approvals.Verify("this should work"));
    }
}