using System;
using System.IO;
using System.Reflection;

namespace ApprovalTests.Namers
{
    public class RelativeToAssemblyNamer : UnitTestFrameworkNamer
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

        public new string SourcePath
        {
            get
            {
                return AssemblyDirectory + GetSubdirectory();
            }
        }
    }
}
