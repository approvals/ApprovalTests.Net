using System.Diagnostics;
using ApprovalTests.Core;
using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Namers
{
    public class UnitTestFrameworkNamer : IApprovalNamer
    {
        private readonly StackTraceParser stackTraceParser;

        public UnitTestFrameworkNamer()
        {
            stackTraceParser = new StackTraceParser();
            stackTraceParser.Parse(new StackTrace(true));
        }

        public string Name
        {
            get { return stackTraceParser.ApprovalName; }
        }

        public string SourcePath
        {
            get { return stackTraceParser.SourcePath; }
        }
    }
}