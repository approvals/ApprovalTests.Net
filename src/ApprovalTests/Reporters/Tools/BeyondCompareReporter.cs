using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class BeyondCompareReporter : DiffToolReporter
    {
        public static readonly BeyondCompareReporter INSTANCE = new BeyondCompareReporter();

        public BeyondCompareReporter()
            : base(DiffTool.BeyondCompare)
        {
        }
    }
}