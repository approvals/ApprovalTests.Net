using NUnit.Framework;
using ApprovalTests.Namers;
using ApprovalTests.Tests.Mocks;
using ApprovalTests.Reporters;

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
                RootNamespace = "MyProject.Tests",
                RootPath = @"Z:\Projects\MyProject\MyProject.Tests",
                Namespace = "MyProject.Tests"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(stackTraceParser);

            Assert.That(namer.SourcePath, Is.EqualTo(@"Z:\Projects\MyProject\MyProject.Tests"));
        }

        [Test]
        public void source_path_is_derived_correctly_when_the_test_namespace_is_below_the_root_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                RootNamespace = "MyProject.Tests",
                RootPath = @"Z:\Projects\MyProject\MyProject.Tests",
                Namespace = "MyProject.Tests.Customers.Api"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(stackTraceParser);

            Assert.That(namer.SourcePath, Is.EqualTo(@"Z:\Projects\MyProject\MyProject.Tests\Customers\Api"));
        }

        [Test]
        public void an_exception_is_thrown_when_there_is_no_match_between_the_root_namespace_and_the_test_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                RootNamespace = "Root.namespace.that.does.not.match.namespace.of.test",
                RootPath = @"C:\Projects\MyProject\MyProject.Tests",
                Namespace = "MyNewProject.Tests"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(stackTraceParser);

            Assert.That(() => namer.SourcePath, Throws.Exception);
        }

        [Test]
        public void an_exception_is_thrown_when_there_is_no_match_between_the_test_namespace_and_the_root_namespace()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                RootNamespace = "MyProject.Tests",
                RootPath = @"C:\Projects\MyProject\MyProject.Tests",
                Namespace = "test.namespace.that.does.not.match.root.namespace"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(stackTraceParser);

            Assert.That(() => namer.SourcePath, Throws.Exception);
        }

        [Test]
        public void an_exception_is_thrown_when_the_root_and_test_namespaces_do_not_match_from_the_beginning()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                RootNamespace = "HelloWorld",
                RootPath = @"C:\Projects\MyProject\MyProject.Tests",
                Namespace = "Microsoft.HelloWorld"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(stackTraceParser);

            Assert.That(() => namer.SourcePath, Throws.Exception);
        }

        [Test]
        [UseApprovalSubdirectory("subdir1")]
        public void source_path_has_subdirectory_in_path()
        {
            var stackTraceParser = new MockStackTraceParser
            {
                RootNamespace = "MyProject.Tests",
                RootPath = @"Z:\Projects\MyProject\MyProject.Tests",
                Namespace = "MyProject.Tests.CRM"
            };

            var namer = new RelativeToUnitTestNamespaceNamer(stackTraceParser);

            Assert.That(namer.SourcePath, Is.EqualTo(@"Z:\Projects\MyProject\MyProject.Tests\CRM\subdir1"));
        }

        [UseNamer(typeof(RelativeToUnitTestNamespaceNamer), ForReporterOfType = typeof(DiffReporter))]
        [UseReporter(typeof(DiffReporter))]
        public class IntegrationTests
        {
            [Test]
            [UseApprovalSubdirectory("Approvals\\Json")]
            public void full_integration_test()
            {
                Approvals.VerifyJson(@"{ 'happy': 'days' }");
            }

            [TearDown]
            public void TearDown()
            {
                Approvals.RegisterDefaultNamerCreation(() => new UnitTestFrameworkNamer());
            }
        }
    }
}