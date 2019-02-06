using ApprovalTests.Reporters;
using System;
using Xunit;
using static ApprovalTests.Xunit2.CompatibilityWithXunit1FixutureTest;

namespace ApprovalTests.Xunit2
{
    public class CompatibilityWithXunit1FixutureTest : IClassFixture<DummyTestStateClass>
    {
        public class DummyTestStateClass : IDisposable
        {
            public string ConnectionString { get; }

            public DummyTestStateClass()
            {
                ConnectionString = "foo";
            }

            public void Dispose(){}
        }

        private string _connectionString;

        public CompatibilityWithXunit1FixutureTest(DummyTestStateClass state)
        {
            _connectionString = state.ConnectionString;
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void Xunit2ShouldWorkWithFixtureTest()
        {
            var content = "{ 'result':'true' }";
            Approvals.Verify(content);
        }

    }
}