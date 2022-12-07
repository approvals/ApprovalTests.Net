namespace ApprovalUtilities.Persistence;

public class SaverAsyncWrapper<T> : ISaverAsync<T>
{
    readonly ISaver<T> saver;

    public SaverAsyncWrapper(ISaver<T> saver)
    {
        this.saver = saver;
    }

    public Task<T> Save(T objectToBeSaved)
    {
        return Task.Factory.StartNew(() => saver.Save(objectToBeSaved));
    }
}