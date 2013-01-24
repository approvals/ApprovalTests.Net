namespace ApprovalTests.Tests.Events
{
    using System;
    using System.ComponentModel;

    public class TestingPoco 
    {
        private readonly object NonEventField = new object();

        public event EventHandler MyEvent;

        private Func<bool> Truth = () => true;

        protected virtual void OnMyEvent(object sender, EventArgs e)
        {
            EventHandler handler = MyEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class InheritsTestingPoco : TestingPoco
    {
    }
}