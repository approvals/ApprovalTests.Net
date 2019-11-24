using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ApprovalTests.Namers
{
    /// <summary>
    /// <see cref="UnitTestFrameworkNamer" /> assumes that the .approved. files exist relative to the original source location (which is derived from <see cref="StackFrame.GetFileName" />).
    /// The issue with this assumption is that the test execution location may, in some cases, differ to the test source location.
    ///
    /// As a concrete example:
    /// * The source location is D:\a\1\s\Foo.Tests\Bar\Baz\Qux.approved.txt
    /// * The execution location is D:\a\r1\a\Foo.Tests\Bar\Baz\Qux.approved.txt
    /// * The test namespace is Foo.Tests.Bar.Baz
    ///
    /// In this example D:\a\1\s\Foo.Tests\Bar\Baz\Qux.approved.txt would not be found (resulting in the test actual being compared against "").
    ///
    /// This Namer resolves this, with the following conditions:
    /// * All test namespaces must start with the test assembly's name. e.g. Foo.Tests
    /// * All test namespaces must align with the on-disk folder structure. e.g. The Foo.Tests.Bar.Baz namespace must be stored on disk as Foo.Tests\Bar\Baz
    /// </summary>
    public class AssemblyLocationAndTestNamespaceNamer : AssemblyLocationNamer
    {
        public AssemblyLocationAndTestNamespaceNamer()
        {
        }

        public override string SourcePath => Path.Combine(AssemblyDirectory, GetSubDirectoryFromTestNamespace());

        private string GetSubDirectoryFromTestNamespace()
        {
            // e.g. Foo.Tests.Bar.Baz
            var testNamespace = ApprovalClass.Namespace;

            // e.g. Foo.Tests
            var testAssemblyName = ApprovalClass.Assembly.GetName().Name;
            var expectedNamespacePrefixRegex = $"^{testAssemblyName}[.]";

            // e.g. Replace Foo.Tests.Bar.Baz with Bar.Baz
            var testNamespaceWithoutAssemblyName =
                Regex.Replace(testNamespace, expectedNamespacePrefixRegex, "");

            // e.g. Replace Bar.Baz with Bar\Baz
            var subDirectory = testNamespaceWithoutAssemblyName.Replace(".", Path.DirectorySeparatorChar.ToString());

            return subDirectory;
        }
    }
}
