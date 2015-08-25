using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;
using System.IO;

namespace ApprovalTests.Namers
{
    public class UnitTestFrameworkNamer : IApprovalNamer
    {
        private readonly string additionalContext;
        private readonly StackTraceParser stackTraceParser;
        private string subdirectory;

        public UnitTestFrameworkNamer()
        {
            Approvals.SetCaller();
            stackTraceParser = new StackTraceParser();
            stackTraceParser.Parse(Approvals.CurrentCaller.StackTrace);
            HandleSubdirectory();
        }

        /// <summary>
        /// For data driven tests and other scenarios where you want multiple approvals per test
        /// </summary>
        /// <param name="additionalContext">Will be added between the test name and the received.ext or approved.ext</param>
        public UnitTestFrameworkNamer(string additionalContext) : this()
        {
            this.additionalContext = additionalContext;
        }

        private void HandleSubdirectory()
        {
            var subdirectoryAttribute = Approvals.CurrentCaller.GetFirstFrameForAttribute<UseApprovalSubdirectoryAttribute>();
            if (subdirectoryAttribute != null)
            {
                subdirectory = subdirectoryAttribute.Subdirectory;
            }
        }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(additionalContext))
                    return string.Concat(stackTraceParser.ApprovalName, ".", additionalContext);
                return stackTraceParser.ApprovalName;
            }
        }

        public string SourcePath
        {
            get { return stackTraceParser.SourcePath + GetSubdirectory(); }
        }

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