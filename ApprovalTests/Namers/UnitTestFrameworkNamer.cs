using System.IO;
using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.Namers
{
    public class UnitTestFrameworkNamer : IApprovalNamer
    {
        private readonly StackTraceParser stackTraceParser;
        private string subdirectory;

        public UnitTestFrameworkNamer()
        {
            Approvals.SetCaller();
            stackTraceParser = new StackTraceParser();
            stackTraceParser.Parse(Approvals.CurrentCaller.StackTrace);
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

        public string Name => stackTraceParser.ApprovalName;

        public string SourcePath => stackTraceParser.SourcePath + GetSubdirectory();

        public string GetSubdirectory()
        {
            if (string.IsNullOrEmpty(subdirectory))
            {
                return string.Empty;
            }
            return Path.DirectorySeparatorChar + subdirectory;
        }
    }
}