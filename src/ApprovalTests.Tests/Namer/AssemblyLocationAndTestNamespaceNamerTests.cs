using System.IO;
using ApprovalTests.Namers;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ApprovalTests.Tests.Namer.With.Additional.Namespace.Nesting
{
    public class AssemblyLocationAndTestNamespaceNamerTests
    {
        [Test]
        public void TestSourcePath()
        {
            var sourcePath = new AssemblyLocationAndTestNamespaceNamer().SourcePath;
            var expectedPath =
                Path.Combine(
                    new AssemblyLocationNamer().SourcePath,
                    @"Namer\With\Additional\Namespace\Nesting");

            Assert.AreEqual(expectedPath, sourcePath);
        }
    }
}
