using DiffEngine;

namespace ApprovalTests.Reporters.Linux
{
    public class MeldReporter : DiffToolReporter
    {
        public static readonly MeldReporter INSTANCE = new MeldReporter();

        public MeldReporter() : base(DiffTool.Meld)
        {

        }
    }
}