using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class KaleidoscopeDiffReporter : DiffToolReporter
    {
        public static readonly KaleidoscopeDiffReporter INSTANCE = new KaleidoscopeDiffReporter();

        public KaleidoscopeDiffReporter() : base(DiffTool.Kaleidoscope)
        {
        }
    }
}