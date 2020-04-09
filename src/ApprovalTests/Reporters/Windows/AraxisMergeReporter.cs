using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class AraxisMergeReporter : DiffToolReporter
    {
        public static readonly AraxisMergeReporter INSTANCE = new AraxisMergeReporter();

        public AraxisMergeReporter()
            : base(DiffTool.AraxisMerge)
        {
        }
    }
}