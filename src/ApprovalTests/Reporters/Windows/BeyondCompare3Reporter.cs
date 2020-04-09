using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class BeyondCompare3Reporter : DiffToolReporter
    {
        public static readonly BeyondCompare3Reporter INSTANCE = new BeyondCompare3Reporter();

        public BeyondCompare3Reporter()
            : base(DiffTool.BeyondCompare)
        {
        }
    }
}