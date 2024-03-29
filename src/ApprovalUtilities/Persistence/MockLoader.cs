namespace ApprovalUtilities.Persistence;

public class MockLoader<T> : ILoader<T>
{
    T t;

    public MockLoader(T t) => this.t = t;

    public T Load() => t;
}