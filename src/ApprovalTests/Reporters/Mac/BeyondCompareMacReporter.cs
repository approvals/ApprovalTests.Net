using DiffEngine;

namespace ApprovalTests.Reporters.Mac
{
    public class BeyondCompareMacReporter : DiffToolReporter
    {
        public static readonly BeyondCompareMacReporter INSTANCE = new BeyondCompareMacReporter();

        public BeyondCompareMacReporter() : base(DiffTool.BeyondCompare)
        {

        }
    }
}