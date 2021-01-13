using System;
using System.Diagnostics;

namespace ApprovalTests.Namers.StackTraceParsers
{
    public interface IStackTraceParser
    {
        string ApprovalName { get; }
        Type ApprovalClass { get; }
        string SourcePath { get; }
        string ForTestingFramework { get; }
        bool Parse(StackTrace stackTrace);
    }
}