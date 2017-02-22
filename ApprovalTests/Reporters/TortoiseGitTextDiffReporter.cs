namespace ApprovalTests.Reporters
{
    public class TortoiseGitTextDiffReporter : GenericDiffReporter
    {
        public static readonly TortoiseGitTextDiffReporter INSTANCE = new TortoiseGitTextDiffReporter();

        public TortoiseGitTextDiffReporter() : base(DiffPrograms.Windows.TORTOISEGIT_TEXT_DIFF)
        {
        }
    }
}