namespace ApprovalTests.Reporters
{
    public class TortoiseImageDiffReporter : GenericDiffReporter
    {
        public static readonly TortoiseImageDiffReporter INSTANCE = new TortoiseImageDiffReporter();

        public TortoiseImageDiffReporter()
            : base(DiffPrograms.Windows.TORTOISE_IMAGE_DIFF)
        {
        }
    }
}