using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class KDiff3Reporter : DiffToolReporter
    {
        public static readonly KDiff3Reporter INSTANCE = new KDiff3Reporter();

        public KDiff3Reporter() : base(DiffTool.KDiff3)
        {
        }
    }
}