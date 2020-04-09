using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class P4MergeImageReporter : DiffToolReporter
    {
        public static readonly P4MergeImageReporter INSTANCE = new P4MergeImageReporter();

        public P4MergeImageReporter()
            : base(DiffTool.P4Merge)
        {
        }
    }
}