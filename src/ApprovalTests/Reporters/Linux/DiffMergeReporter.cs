using DiffEngine;

namespace ApprovalTests.Reporters.Linux
{
    public class DiffMergeReporter : DiffToolReporter
    {
        public static readonly DiffMergeReporter INSTANCE = new DiffMergeReporter();

        public DiffMergeReporter() : base(DiffTool.DiffMerge)
        {
        }
    }
}