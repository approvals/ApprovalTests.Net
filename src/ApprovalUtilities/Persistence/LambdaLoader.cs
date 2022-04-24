using System;

namespace ApprovalUtilities.Persistence;

public class LambdaLoader<T> : ILoader<T>
{
    readonly Func<T> func;

    public LambdaLoader(Func<T> func)
    {
        this.func = func;
    }

    public T Load()
    {
        return func.Invoke();
    }
}