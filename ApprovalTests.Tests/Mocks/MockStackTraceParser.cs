using System.Diagnostics;
using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.Tests.Mocks
{
    public class MockStackTraceParser : IStackTraceParser
    {
        public string ApprovalName { get; set; }
        public string ForTestingFramework { get; set; }
        public string Namespace { get; set; }
        public string SourcePath { get; set; }
        public bool Parse(StackTrace stackTrace)
        {
            return true;
        }
    }
}