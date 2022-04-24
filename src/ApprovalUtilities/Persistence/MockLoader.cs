namespace ApprovalUtilities.Persistence;

public class MockLoader<T> : ILoader<T>
{
    private T t;

    public MockLoader(T t)
    {
        this.t = t;
    }

    public T Load()
    {
        return t;
    }
}