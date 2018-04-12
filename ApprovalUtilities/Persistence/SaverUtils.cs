namespace ApprovalUtilities.Persistence
{
    public static class SaverUtils
    {
        public static ISaverAsync<T> ToAsync<T>(this ISaver<T> saver)
        {
            return new SaverAsyncWrapper<T>(saver);
        }

        public static ISaver<T> ToSynchronous<T>(this ISaverAsync<T> saver)
        {
            return new SaverSynchronousWrapper<T>(saver);
        }
    }
}