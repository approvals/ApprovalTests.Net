using NUnit.Framework;
using ApprovalTests.Namers;
using ApprovalTests.Tests.Mocks;

namespace ApprovalTests.Tests.Namer
{
    [TestFixture]
    class RelativeToUnitTestNamespaceNamerTests
    {
        [Test]
        public void source_path_is_derived_correctly_when_the_test_namespace_is_the_same_as_the_root_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                Namespace = "MyProject.Tests"
            };

            var namer = new RelativeToUnitTestNamespaceNamer("MyProject.Tests", @"Z:\Projects\MyProject\MyProject.Tests", stackTraceParser);

            Assert.That(namer.SourcePath, Is.EqualTo(@"Z:\Projects\MyProject\MyProject.Tests"));
        }

        [Test]
        public void source_path_is_derived_correctly_when_the_test_namespace_is_below_the_root_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                Namespace = "MyProject.Tests.Customers.Api"
            };

            var namer = new RelativeToUnitTestNamespaceNamer("MyProject.Tests", @"Z:\Projects\MyProject\MyProject.Tests", stackTraceParser);

            Assert.That(namer.SourcePath, Is.EqualTo(@"Z:\Projects\MyProject\MyProject.Tests\Customers\Api"));
        }

        [Test]
        public void an_exception_is_thrown_when_there_is_no_match_between_the_root_namespace_and_the_test_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                Namespace = "MyNewProject.Tests"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(
                rootNamespace: "Root.namespace.that.does.not.match.namespace.of.test",
                assemblyPath: @"C:\Projects\MyProject\MyProject.Tests",
                stackTraceParser: stackTraceParser);

            Assert.That(() => namer.SourcePath, Throws.Exception);
        }

        [Test]
        public void an_exception_is_thrown_when_there_is_no_match_between_the_test_namespace_and_the_root_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                Namespace = "test.namespace.that.does.not.match.root.namespace"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(
                rootNamespace: "MyProject.Tests",
                assemblyPath: @"C:\Projects\MyProject\MyProject.Tests",
                stackTraceParser: stackTraceParser);

            Assert.That(() => namer.SourcePath, Throws.Exception);
        }

        [Test]
        public void an_exception_is_thrown_when_the_root_and_test_namespaces_do_not_match_from_the_beginning()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                Namespace = "Microsoft.HelloWorld"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(
                rootNamespace: "HelloWorld",
                assemblyPath: @"C:\Projects\MyProject\MyProject.Tests",
                stackTraceParser: stackTraceParser);

            Assert.That(() => namer.SourcePath, Throws.Exception);
        }
    }
}