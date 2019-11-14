namespace ApprovalTests.Reporters.Windows
{
    public class TortoiseTextDiffReporter : GenericDiffReporter
    {
        public static readonly TortoiseTextDiffReporter INSTANCE = new TortoiseTextDiffReporter();

        public TortoiseTextDiffReporter(): base(DiffPrograms.Windows.TORTOISE_TEXT_DIFF)
        {
        }
    }
}