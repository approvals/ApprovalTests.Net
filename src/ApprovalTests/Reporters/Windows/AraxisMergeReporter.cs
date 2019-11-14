
namespace ApprovalTests.Reporters.Windows
{
    public class AraxisMergeReporter : GenericDiffReporter
    {
        public static readonly AraxisMergeReporter INSTANCE = new AraxisMergeReporter();

        public AraxisMergeReporter()
            : base(DiffPrograms.Windows.ARAXIS_MERGE)
        {
        }
    }
}