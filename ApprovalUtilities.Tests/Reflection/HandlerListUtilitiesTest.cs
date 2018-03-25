#if !NETCORE
namespace ApprovalUtilities.Tests.Reflection
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using ApprovalTests;
    using ApprovalTests.Tests.Events;
    using NUnit.Framework;
    using ApprovalUtilities.Reflection;

    public class HandlerListUtilitiesTest
    {
        [Test]
        public void EnumerateList()
        {
            Approvals.VerifyAll(GetEventHandlerList().AsEnumerable(), string.Empty);
        }

        [Test]
        public void GetListHead()
        {
            Approvals.Verify(GetEventHandlerList().GetHead());
        }

        private static EventHandlerList GetEventHandlerList()
        {
            var button = new Button();
            button.Click += TestingListener.AnotherStandardCallback;
            button.GotFocus += TestingListener.StandardCallback;
            var eventListInfo = button.NonPublicInstanceProperties(pi => pi.Name == "Events").Single();
            return eventListInfo.GetValue<EventHandlerList>(button, null);
        }
    }
}
#endif