namespace ApprovalUtilities.Persistence;

public class SaverSynchronousWrapper<T> : ISaver<T>
{
    private readonly ISaverAsync<T> saver;

    public SaverSynchronousWrapper(ISaverAsync<T> saver)
    {
        this.saver = saver;
    }

    public T Save(T objectToBeSaved)
    {
        return saver.Save(objectToBeSaved).Result;
    }
}