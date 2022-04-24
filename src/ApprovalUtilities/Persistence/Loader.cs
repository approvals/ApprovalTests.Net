using System;

namespace ApprovalUtilities.Persistence;

public class Loader<T> : ILoader<T>
{
    private readonly Func<T> load;

    public Loader(T item) : this(() => item)
    {
    }

    public Loader(Func<T> load)
    {
        this.load = load;
    }

    public T Load()
    {
        return load();
    }
}