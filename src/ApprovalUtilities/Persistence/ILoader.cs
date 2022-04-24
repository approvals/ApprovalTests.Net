namespace ApprovalUtilities.Persistence;

public interface ILoader<T>
{
    T Load();
}