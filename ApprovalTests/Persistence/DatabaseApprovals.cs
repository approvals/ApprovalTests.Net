using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.Persistence
{
    public class DatabaseApprovals
    {
        public static void Verify(IDatabaseToExecuteableQueryAdaptor adapter)
        {
            Approvals.Verify(new ExecutableSqlQuery(adapter));
        }
    }
}