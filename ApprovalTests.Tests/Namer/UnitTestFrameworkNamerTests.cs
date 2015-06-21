using NUnit.Framework;
using ApprovalTests.Tests.Mocks;
using ApprovalTests.Namers;

namespace ApprovalTests.Tests.Namer
{
    [TestFixture]
    class UnitTestFrameworkNamerTests
    {
        [Test]
        public void name_is_equivalent_to_the_value_derived_by_the_stack_trace_parser()
        {
            var mockStackTraceParser = new MockStackTraceParser { ApprovalName = "approval name" };
            var namer = new UnitTestFrameworkNamer(mockStackTraceParser);
            Assert.That(namer.Name, Is.EqualTo(mockStackTraceParser.ApprovalName));
        }

        [Test]
        public void source_path_is_equivalent_to_the_value_derived_by_the_stack_trace_parser()
        {
            var mockStackTraceParser = new MockStackTraceParser { SourcePath = "source path" };
            var namer = new UnitTestFrameworkNamer(mockStackTraceParser);
            Assert.That(namer.SourcePath, Is.EqualTo(mockStackTraceParser.SourcePath));
        }
    }
}