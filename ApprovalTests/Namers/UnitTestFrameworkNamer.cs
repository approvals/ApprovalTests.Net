using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;
using System.IO;

namespace ApprovalTests.Namers
{
    public class UnitTestFrameworkNamer : IApprovalNamer
    {
        private string subdirectory;

        public UnitTestFrameworkNamer() : this(new StackTraceParser())
        {
        }

        public UnitTestFrameworkNamer(IStackTraceParser stackTraceParser)
        {
            Approvals.SetCaller();
            StackTraceParser = stackTraceParser;
            StackTraceParser.Parse(Approvals.CurrentCaller.StackTrace);
            HandleSubdirectory();
        }

        private void HandleSubdirectory()
        {
            var subdirectoryAttribute = Approvals.CurrentCaller.GetFirstFrameForAttribute<UseApprovalSubdirectoryAttribute>();
            if (subdirectoryAttribute != null)
            {
                subdirectory = subdirectoryAttribute.Subdirectory;
            }
        }

        public virtual string Name
        {
            get { return StackTraceParser.ApprovalName; }
        }

        public virtual string SourcePath
        {
            get { return StackTraceParser.SourcePath + GetSubdirectory(); }
        }

        public string GetSubdirectory()
        {
            if (string.IsNullOrEmpty(subdirectory))
            {
                return string.Empty;
            }
            return Path.DirectorySeparatorChar + subdirectory;
        }

        protected IStackTraceParser StackTraceParser
        {
            get; private set;
        }
    }
}