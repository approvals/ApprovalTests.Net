public class TestingEventPoco
{
    readonly object NonEventField = new();

    Func<bool> Truth = () => true;

    public event EventHandler MyEvent;

#pragma warning disable 67

    public event PropertyChangedEventHandler PropertyChanged;

#pragma warning restore 67

    protected virtual void OnMyEvent(object sender, EventArgs e)
    {
        var handler = MyEvent;
        handler?.Invoke(sender, e);
    }
}