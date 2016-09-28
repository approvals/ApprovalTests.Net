namespace ApprovalTests.Reporters
{
    public class BeyondCompare4Reporter : GenericDiffReporter
    {
        public readonly static BeyondCompare4Reporter INSTANCE = new BeyondCompare4Reporter();

        public BeyondCompare4Reporter()
            : base(DiffPrograms.Windows.BEYOND_COMPARE_4)
        {
        }
    }
}