namespace ApprovalTests.Tests.Events
{
    using System;
    using System.Windows.Forms;

    public class TestingControl : Control
    {
        private static readonly object EventKey = new object();

        private readonly object NonEventField = new object();

        public event EventHandler KeyEvent
        {
            add => Events.AddHandler(EventKey, value);
            remove => Events.RemoveHandler(EventKey, value);
        }

        public event EventHandler MyEvent;

        protected virtual void OnMyEvent(object sender, EventArgs e)
        {
            var handler = MyEvent;
            handler?.Invoke(sender, e);
        }
    }
}