namespace ApprovalUtilities.Persistence
{
    public interface IExecutableQuery
    {
        string GetQuery();

        string ExecuteQuery(string query);
    }
}