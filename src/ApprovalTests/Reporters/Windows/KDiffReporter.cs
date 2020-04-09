using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class KDiffReporter : DiffToolReporter
    {
        public static readonly KDiffReporter INSTANCE = new KDiffReporter();

        public KDiffReporter()
            : base(DiffTool.KDiff3)
        {
        }
    }
}