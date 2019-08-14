namespace ApprovalTests.Reporters.Linux
{
    public class MeldReporter:GenericDiffReporter
    {

        public static readonly MeldReporter INSTANCE = new MeldReporter();

        public MeldReporter() : base(DiffPrograms.Linux.MELD)
        {

        }
    }
}