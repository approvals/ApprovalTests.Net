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

        private string GetSubdirectoryFromAttribute()
        {
            var subdirectoryAttribute = Approvals.CurrentCaller.GetFirstFrameForAttribute<UseApprovalSubdirectoryAttribute>();
            return subdirectoryAttribute == null ? string.Empty : subdirectoryAttribute.Subdirectory;
        }

        public string Name => stackTraceParser.ApprovalName;

        public virtual string SourcePath => Path.Combine(stackTraceParser.SourcePath, Subdirectory);

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Subdirectory))]
        public string GetSubdirectory()
        {
            return Subdirectory;
        }
    }
}