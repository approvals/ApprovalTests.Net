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
    public class HandlerListUtilitiesTest
    {
        [TestMethod]
        public void EnumerateList()
        {
            Approvals.VerifyAll(GetEventHandlerList().AsEnumerable(), string.Empty);
        }

        [TestMethod]
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