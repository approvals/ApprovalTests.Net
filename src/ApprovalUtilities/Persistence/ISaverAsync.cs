namespace ApprovalUtilities.Persistence;

public interface ISaverAsync<T>
{
    Task<T> Save(T objectToBeSaved);
}