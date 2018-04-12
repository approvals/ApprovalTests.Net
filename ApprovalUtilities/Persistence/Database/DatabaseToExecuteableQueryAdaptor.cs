using System.Data.Common;

namespace ApprovalUtilities.Persistence.Database
{
    public interface IDatabaseToExecuteableQueryAdaptor
    {
        string GetQuery();
        DbConnection GetConnection();
    }
}