namespace ApprovalUtilities.Persistence;

public interface ISaver<T>
{
    T Save(T objectToBeSaved);
}