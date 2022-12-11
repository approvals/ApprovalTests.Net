namespace ApprovalUtilities.Persistence;

public class SaverSynchronousWrapper<T> : ISaver<T>
{
    readonly ISaverAsync<T> saver;

    public SaverSynchronousWrapper(ISaverAsync<T> saver) =>
        this.saver = saver;

    public T Save(T objectToBeSaved) =>
        saver.Save(objectToBeSaved).Result;
}