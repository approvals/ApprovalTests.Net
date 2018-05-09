using System;
using System.IO;
using System.Reflection;

namespace ApprovalTests.Namers
{
    public class AssemblyLocationNamer : UnitTestFrameworkNamer
    {
        private string AssemblyDirectory
        {
            get
            {
                // CodeBase is used because the NUnit test runner is otherwise quirky
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public override string SourcePath => AssemblyDirectory + GetSubdirectory();
    }
}