namespace ApprovalTests.Reporters.Mac
{
    public class BeyondCompareMacReporter : GenericDiffReporter
    {

        public static readonly BeyondCompareMacReporter INSTANCE = new BeyondCompareMacReporter();

        public BeyondCompareMacReporter() : base(DiffPrograms.Mac.BEYOND_COMPARE)
        {

        }
    }
}