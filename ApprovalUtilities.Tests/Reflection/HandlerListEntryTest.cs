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

    public class HandlerListEntryTest
    {
        [Test]
        public void BecomeNullObjectWhenItemIsWrongType()
        {
            Approvals.Verify(new HandlerListEntry(new Button()));
        }

        [Test]
        public void GetListEntryTest()
        {
            Assert.AreEqual("ListEntry", GetListEntry().GetType().Name);
        }

        [Test]
        public void ProxyNonPublicMembers()
        {
            Approvals.Verify(new HandlerListEntry(GetListEntry()));
        }

        private static object GetListEntry()
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
}
#endif