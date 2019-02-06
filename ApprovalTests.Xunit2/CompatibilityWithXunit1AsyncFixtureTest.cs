using ApprovalTests.Reporters;
using System;
using System.Threading.Tasks;
using Xunit;
using static ApprovalTests.Xunit2.CompatibilityWithXunit1AsyncFixutureTest;

namespace ApprovalTests.Xunit2
{
    public class CompatibilityWithXunit1AsyncFixutureTest : IClassFixture<DummyTestStateClass>
    {
        public class DummyTestStateClass : IDisposable
        {
            public string ConnectionString { get; }

            public DummyTestStateClass()
            {
                ConnectionString = "foo";
            }

            public void Dispose() { }
        }

        private string _connectionString;

        public CompatibilityWithXunit1AsyncFixutureTest(DummyTestStateClass state)
        {
            _connectionString = state.ConnectionString;
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async void Xunit2ShouldWorkWithAsyncFixtureTest()
        {
            // pretend we're doing something async so that weforce the compiler to do it's 'async' thing.
            var json = await Task.Run<string>(async () =>
            {
                // pretend we used the connection string to connect to a database and called some ExecuteAsync method
                await Task.Delay(10);
                var content = "{ 'result':'true' }";

                // the call below to Verify inside the ansyc method fails
                // fails with message "Could Not Detect Test Framework"
                Approvals.Verify(content);
                return content;
            });

            // if we move the approve outside of the async call and not call it inside the async method, then it works correctly.
            //Approvals.Verify(json);
        }

    }
}