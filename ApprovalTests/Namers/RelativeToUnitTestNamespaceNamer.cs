using System;
using System.IO;
using System.Reflection;
using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.Namers
{
    public class RelativeToUnitTestNamespaceNamer : UnitTestFrameworkNamer
    {
        private string rootNamespace;
        private string assemblyPath;

        public RelativeToUnitTestNamespaceNamer()
        {
            var assembly = Assembly.GetExecutingAssembly();
            rootNamespace = assembly.GetName().Name;
            assemblyPath = GetAssemblyDirectory(assembly);
        }

        public RelativeToUnitTestNamespaceNamer(string rootNamespace, string assemblyPath, IStackTraceParser stackTraceParser)
            : base(stackTraceParser)
        {
            this.rootNamespace = rootNamespace;
            this.assemblyPath = assemblyPath;
        }

        public override string SourcePath
        {
            get { return assemblyPath + GetPathRelativeToAssemblyBasedOnNamespace() + GetSubdirectory(); }
        }

        private string GetPathRelativeToAssemblyBasedOnNamespace()
        {
            var rootNamespace = this.rootNamespace;
            var testNamespace = StackTraceParser.Namespace;
            
            if (!string.IsNullOrEmpty(testNamespace) && testNamespace.IndexOf(rootNamespace) == 0)
            {
                var relativeNamespace = testNamespace.Replace(rootNamespace, "");
                return relativeNamespace.Replace(".", "\\");
            }
            throw new Exception("Unable to derive source path. To use the RelativeToUnitTestNamespaceNamer, your tests must be directly in, or below, the root namespace (assembly name).");
        }

        private static string GetAssemblyDirectory(Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}