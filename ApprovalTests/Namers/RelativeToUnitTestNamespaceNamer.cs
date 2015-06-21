using System;
using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.Namers
{
    public class RelativeToUnitTestNamespaceNamer : UnitTestFrameworkNamer
    {
        public RelativeToUnitTestNamespaceNamer(IStackTraceParser stackTraceParser)
            : base(stackTraceParser)
        {
        }

        public RelativeToUnitTestNamespaceNamer()
            : this(new StackTraceParser())
        {
        }

        public override string SourcePath
        {
            get { return StackTraceParser.RootPath + GetPathRelativeToAssemblyBasedOnNamespace() + GetSubdirectory(); }
        }

        private string GetPathRelativeToAssemblyBasedOnNamespace()
        {
            if (!string.IsNullOrEmpty(StackTraceParser.Namespace) && StackTraceParser.Namespace.IndexOf(StackTraceParser.RootNamespace) == 0)
            {
                var relativeNamespace = StackTraceParser.Namespace.Replace(StackTraceParser.RootNamespace, "");
                return relativeNamespace.Replace(".", "\\");
            }
            throw new Exception("Unable to derive source path. To use the RelativeToUnitTestNamespaceNamer, your tests must be directly in, or below, the root namespace (assembly name).");
        }
    }
}