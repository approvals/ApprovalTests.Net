namespace ApprovalUtilities.Utilities;

public class Disposables : IDisposable
{
    readonly Action _onDispose;

    Disposables(Action onDispose) =>
        _onDispose = onDispose;

    public void Dispose() =>
        _onDispose();

    public static IDisposable Create(Action on_dispose) =>
        new Disposables(on_dispose);
}