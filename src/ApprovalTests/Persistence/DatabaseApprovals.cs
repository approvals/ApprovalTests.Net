using ApprovalUtilities.Persistence.Database;

namespace ApprovalTests.Persistence
{
    public class DatabaseApprovals
    {
        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "DatabaseApprovals.Verify(IDatabaseToExecutableQueryAdapter)")]
        public static void Verify(IDatabaseToExecuteableQueryAdaptor adapter)
        {
            Approvals.Verify(new ExecutableSqlQuery(adapter));
        }

        public static void Verify(IDatabaseToExecutableQueryAdapter adapter)
        {
            Approvals.Verify(new ExecutableSqlQuery(adapter));
        }
    }
}