using System;
using System.ComponentModel;

namespace ApprovalTests.Tests.Events
{
    public class TestingEventPoco
    {
        private readonly object NonEventField = new object();

        private Func<bool> Truth = () => true;

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
}