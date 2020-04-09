using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class TortoiseGitTextDiffReporter : DiffToolReporter
    {
        public static readonly TortoiseGitTextDiffReporter INSTANCE = new TortoiseGitTextDiffReporter();

        public TortoiseGitTextDiffReporter() : base(DiffTool.TortoiseGitMerge)
        {
        }
    }
}