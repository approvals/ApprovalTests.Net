using DiffEngine;

namespace ApprovalTests.Reporters
{
    public class MeldReporter : DiffToolReporter
    {
        public static readonly MeldReporter INSTANCE = new MeldReporter();

        public MeldReporter() : base(DiffTool.Meld)
        {
        }
    }
}