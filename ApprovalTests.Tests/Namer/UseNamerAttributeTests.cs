using System;
using ApprovalTests.Core;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
    [TestFixture]
    class UseNamerAttributeTests
    {
        internal static Func<bool> IsAlwaysTrue = () => true;

        internal class MockNamer : IApprovalNamer
        {
            public string SourcePath { get; private set; }
            public string Name { get; private set; }
        }

        internal static class MockApprovals
        {
            private static Func<IApprovalNamer> _namerCreator;

            public static void SetNamerCreator(Func<IApprovalNamer> namerFunc)
            {
                _namerCreator = namerFunc;
            }

            public static Func<IApprovalNamer> GetNamerCreator()
            {
                return _namerCreator;
            }
        }

        [Test]
        public void CreateNamer_NullUseForCurrentReporter_CreatesDefaultNamer()
        {
            var namer = UseNamerAttribute.CreateNamer(reporter: null, useForCurrentReporter: null);
            Assert.IsInstanceOf<UnitTestFrameworkNamer>(namer);
        }

        [Test]
        public void CreateNamer_NullReporter_CreatesDefaultNamer()
        {
            var namer = UseNamerAttribute.CreateNamer(reporter: null, useForCurrentReporter: IsAlwaysTrue);
            Assert.IsInstanceOf<UnitTestFrameworkNamer>(namer);
        }

        [Test]
        public void CreateNamer_SpecificReporterType_CreatesInstanceOfSpecificReporter()
        {
            var namer = UseNamerAttribute.CreateNamer(reporter: typeof(MockNamer), useForCurrentReporter: IsAlwaysTrue);
            Assert.IsInstanceOf<MockNamer>(namer);
        }

        [Test]
        public void MatchTypeAgainstCurrentReporter_ReporterTypeMatchesTypeOfCurrentReporter_ReturnTrue()
        {
            UseNamerAttribute.CurrentReporterRetrievalFunc = () => new DiffReporter();
            UseNamerAttribute.RegisterNamerCreatorAction = MockApprovals.SetNamerCreator;
            var attribute = new UseNamerAttribute(typeof(MockNamer));
            attribute.ForReporterOfType = typeof(DiffReporter);

            var result = attribute.MatchTypeAgainstCurrentReporter();

            Assert.IsTrue(result);
        }

        [Test]
        public void MatchTypeAgainstCurrentReporter_ReporterTypeDoesNotMatchTypeOfCurrentReporter_ReturnFalse()
        {
            UseNamerAttribute.CurrentReporterRetrievalFunc = () => new DiffReporter();
            UseNamerAttribute.RegisterNamerCreatorAction = MockApprovals.SetNamerCreator;
            var attribute = new UseNamerAttribute(typeof(MockNamer));
            attribute.ForReporterOfType = typeof(NUnitReporter);

            var result = attribute.MatchTypeAgainstCurrentReporter();

            Assert.IsFalse(result);
        }

        [Test]
        public void Constructor_OverwritesTheDefaultNamerCreator()
        {
            UseNamerAttribute.CurrentReporterRetrievalFunc = () => null;
            UseNamerAttribute.RegisterNamerCreatorAction = MockApprovals.SetNamerCreator;

            new UseNamerAttribute(typeof(MockNamer));

            Assert.IsNotNull(MockApprovals.GetNamerCreator());
        }
    }
}
