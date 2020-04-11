using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class TkDiffReporter : DiffToolReporter
    {
        public static readonly TkDiffReporter INSTANCE = new TkDiffReporter();

        public TkDiffReporter() : base(DiffTool.TkDiff)
        {
        }
    }
}