namespace ApprovalTests.Reporters.Mac
{
    public class KDiff3Reporter : GenericDiffReporter
    {

        public static readonly KDiff3Reporter INSTANCE = new KDiff3Reporter();

        public KDiff3Reporter() : base(DiffPrograms.Mac.KDIFF3)
        {

        }
    }
}