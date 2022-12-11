using System.ComponentModel;
using System.Windows.Forms;
using ApprovalUtilities.Reflection;

public class HandlerListEntryTest
{
    [Fact]
    public void BecomeNullObjectWhenItemIsWrongType() =>
        Approvals.Verify(new HandlerListEntry(new Button()));

    [Fact]
    public void GetListEntryTest() =>
        Assert.Equal("ListEntry", GetListEntry().GetType().Name);

    [Fact]
    public void ProxyNonPublicMembers() =>
        Approvals.Verify(new HandlerListEntry(GetListEntry()));

    static object GetListEntry()
    {
        var button = new Button();
        button.Click += TestingListener.AnotherStandardCallback;
        button.GotFocus += TestingListener.StandardCallback;
        var eventListInfo = button.NonPublicInstanceProperties(pi => pi.Name == "Events").Single();
        var eventList = eventListInfo.GetValue<EventHandlerList>(button, null);
        var headInfo = eventList.GetInstanceFields(fi => fi.Name == "head").Single();
        return headInfo.GetValue(eventList);
    }
}