using ApprovalTests.Reporters;
using Xunit;

namespace ApprovalTests.Xunit2;

[UseReporter(typeof(DiffReporter))]
public class CompatibilityWithXunit1AsyncTest
{
    [Fact]
    public async Task Xunit2ShouldWorkWithAsyncTest()
    {
        // do something async so that the compiler does it's 'async' thing.
        var json = await Task.Run(async () =>
        {
            await Task.Delay(10);
            return "{ 'result':'true' }";
        });
        Approvals.Verify(json);
    }
}