namespace ApprovalUtilities.Persistence;

public class Loader<T> : ILoader<T>
{
    readonly Func<T> load;

    public Loader(T item) : this(() => item)
    {
    }

    public Loader(Func<T> load) => this.load = load;

    public T Load() => load();
}