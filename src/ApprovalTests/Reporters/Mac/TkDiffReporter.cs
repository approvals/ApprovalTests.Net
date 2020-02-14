namespace ApprovalTests.Reporters.Mac
{
    public class TkDiffReporter : GenericDiffReporter
    {

        public static readonly TkDiffReporter INSTANCE = new TkDiffReporter();

        public TkDiffReporter() : base(DiffPrograms.Mac.TK_DIFF)
        {

        }
    }
}