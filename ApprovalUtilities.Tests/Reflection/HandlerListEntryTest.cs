namespace ApprovalUtilities.Tests.Reflection
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using ApprovalTests;
    using ApprovalTests.Tests.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ApprovalUtilities.Reflection;

    [TestClass]
    public class HandlerListEntryTest
    {
        [TestMethod]
        public void BecomeNullObjectWhenItemIsWrongType()
        {
            Approvals.Verify(new HandlerListEntry(new Button()));
        }

        [TestMethod]
        public void GetListEntryTest()
        {
            Assert.AreEqual("ListEntry", GetListEntry().GetType().Name);
        }

        [TestMethod]
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