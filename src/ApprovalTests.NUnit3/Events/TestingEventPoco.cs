using System.ComponentModel;

public class TestingEventPoco : INotifyPropertyChanged
{
    readonly object NonEventField = new();

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