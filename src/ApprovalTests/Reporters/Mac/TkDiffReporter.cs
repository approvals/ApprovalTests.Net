using DiffEngine;

namespace ApprovalTests.Reporters.Mac
{
    public class TkDiffReporter : DiffToolReporter
    {
        public static readonly TkDiffReporter INSTANCE = new TkDiffReporter();

        public TkDiffReporter() : base(DiffTool.TkDiff)
        {

        }
    }
}