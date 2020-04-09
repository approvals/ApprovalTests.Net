using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class BeyondCompare4Reporter : DiffToolReporter
    {
        public readonly static BeyondCompare4Reporter INSTANCE = new BeyondCompare4Reporter();

        public BeyondCompare4Reporter()
            : base(DiffTool.BeyondCompare)
        {
        }
    }
}