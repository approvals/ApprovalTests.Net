using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ApprovalTests;
using Xunit;
using ApprovalUtilities.Reflection;

namespace ApprovalUtilities.Tests.Reflection;

public class HandlerListUtilitiesTest
{
    [Fact]
    public void EnumerateList()
    {
        Approvals.VerifyAll(GetEventHandlerList().AsEnumerable(), string.Empty);
    }

    [Fact]
    public void GetListHead()
    {
        Approvals.Verify(GetEventHandlerList().GetHead());
    }

    static EventHandlerList GetEventHandlerList()
    {
        var button = new Button();
        button.Click += TestingListener.AnotherStandardCallback;
        button.GotFocus += TestingListener.StandardCallback;
        var eventListInfo = button.NonPublicInstanceProperties(pi => pi.Name == "Events").Single();
        return eventListInfo.GetValue<EventHandlerList>(button, null);
    }
}