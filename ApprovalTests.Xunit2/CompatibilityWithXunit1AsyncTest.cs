using ApprovalTests.Reporters;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace ApprovalTests.Xunit2
{
    [UseReporter(typeof(DiffReporter))]
    public class CompatibilityWithXunit1AsyncTest
    {
        [Fact]
        public async Task Xunit2ShouldWorkWithAsyncTest()
        {
            // pretend we're doing something async so that weforce the compiler to do it's 'async' thing.
            var json = await Task.Run<string>(async () =>
            {
                await Task.Delay(10);
                return "{ 'result':'true' }";
            });
            Approvals.Verify(json);
        }

    }
}