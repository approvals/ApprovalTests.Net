using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class RiderReporter : DiffToolReporter
    {
        public static readonly RiderReporter INSTANCE = new RiderReporter();

        public RiderReporter()
            : base(DiffTool.P4Merge)
        {
        }
    }
}