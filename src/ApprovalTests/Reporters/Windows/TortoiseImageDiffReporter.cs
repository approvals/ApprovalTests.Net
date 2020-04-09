using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class TortoiseImageDiffReporter : DiffToolReporter
    {
        public static readonly TortoiseImageDiffReporter INSTANCE = new TortoiseImageDiffReporter();

        public TortoiseImageDiffReporter()
            : base(DiffTool.TortoiseIDiff)
        {
        }
    }
}