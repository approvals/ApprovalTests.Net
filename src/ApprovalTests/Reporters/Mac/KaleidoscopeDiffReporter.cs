namespace ApprovalTests.Reporters.Mac
{
    public class KaleidoscopeDiffReporter : GenericDiffReporter
    {

        public static readonly KaleidoscopeDiffReporter INSTANCE = new KaleidoscopeDiffReporter();

        public KaleidoscopeDiffReporter() : base(DiffPrograms.Mac.KALEIDOSCOPE)
        {

        }
    }
}