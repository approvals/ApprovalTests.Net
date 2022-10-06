namespace ApprovalUtilities.Persistence.Database;

public interface IDatabaseToExecutableQueryAdapter
{
    string GetQuery();
    DbConnection GetConnection();
}