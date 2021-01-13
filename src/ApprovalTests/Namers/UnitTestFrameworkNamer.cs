using System;
using System.IO;
using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.Namers
{
    public class UnitTestFrameworkNamer : IApprovalNamer
    {
        private readonly StackTraceParser stackTraceParser;
        public string Subdirectory { get; }

        public UnitTestFrameworkNamer()
        {
            Approvals.SetCaller();
            stackTraceParser = new StackTraceParser();
            stackTraceParser.Parse(Approvals.CurrentCaller.StackTrace);
            Subdirectory = GetSubdirectoryFromAttribute();
        }

        static string GetSubdirectoryFromAttribute()
        {
            var subdirectoryAttribute = Approvals.CurrentCaller.GetFirstFrameForAttribute<UseApprovalSubdirectoryAttribute>();
            return subdirectoryAttribute == null ? string.Empty : subdirectoryAttribute.Subdirectory;
        }

        public string Name => stackTraceParser.ApprovalName;

        protected Type ApprovalClass => stackTraceParser.ApprovalClass;

        public virtual string SourcePath => Path.Combine(stackTraceParser.SourcePath, Subdirectory);
    }
}