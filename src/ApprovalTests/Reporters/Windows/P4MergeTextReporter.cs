using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class P4MergeTextReporter : DiffToolReporter
    {
        public static readonly P4MergeTextReporter INSTANCE = new P4MergeTextReporter();

        public P4MergeTextReporter()
            : base(DiffTool.P4Merge)
        {
        }
    }
}