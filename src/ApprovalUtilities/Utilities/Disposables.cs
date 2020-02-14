using System;

namespace ApprovalUtilities.Utilities
{
    public class Disposables : IDisposable
    {
        private readonly Action _onDispose;

        private Disposables(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose();
        }

        public static IDisposable Create(Action on_dispose)
        {
            return new Disposables(on_dispose);
        }
    }
}