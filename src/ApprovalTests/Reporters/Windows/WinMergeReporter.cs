using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class WinMergeReporter : DiffToolReporter
    {
        public static readonly WinMergeReporter INSTANCE = new WinMergeReporter();

        public WinMergeReporter()
            : base(DiffTool.WinMerge)
        {
        }
    }
}