namespace ApprovalUtilities.Persistence.Database
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(IDatabaseToExecutableQueryAdapter))]
    public interface IDatabaseToExecuteableQueryAdaptor :
        IDatabaseToExecutableQueryAdapter
    {
    }
}