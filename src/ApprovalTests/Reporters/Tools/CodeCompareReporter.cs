using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class CodeCompareReporter : DiffToolReporter
    {
        public static readonly CodeCompareReporter INSTANCE = new CodeCompareReporter();

        public CodeCompareReporter() : base(DiffTool.CodeCompare)
        {
        }
    }
}