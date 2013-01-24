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
            add
            {
                base.Events.AddHandler(TestingControl.EventKey, value);
            }
            remove
            {
                base.Events.RemoveHandler(TestingControl.EventKey, value);
            }
        }

        public event EventHandler MyEvent;

        protected virtual void OnMyEvent(object sender, EventArgs e)
        {
            EventHandler handler = MyEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}