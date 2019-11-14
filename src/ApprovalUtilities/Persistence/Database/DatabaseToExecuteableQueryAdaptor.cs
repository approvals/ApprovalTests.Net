using System.Data.Common;

namespace ApprovalUtilities.Persistence.Database
{
    public interface IDatabaseToExecutableQueryAdapter
    {
        string GetQuery();
        DbConnection GetConnection();
    }
}