namespace ApprovalUtilities.Persistence.Database
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(IDatabaseToExecutableQueryAdapter),
        TreatAsErrorFromVersion = "5.0")]
    public interface IDatabaseToExecuteableQueryAdaptor :
        IDatabaseToExecutableQueryAdapter
    {
    }
}