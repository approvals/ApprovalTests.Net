namespace ApprovalTests.Tests.Events
{
    using System;
    using System.ComponentModel;

    public class TestingPoco : INotifyPropertyChanged
    {
        private readonly object NonEventField = new object();

        public event EventHandler MyEvent;

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
}