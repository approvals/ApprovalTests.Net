namespace ApprovalTests.Tests.Events
{
    using System;
    using System.ComponentModel;

    public class TestingPoco : INotifyPropertyChanged
    {
        private readonly object NonEventField = new object();

        public event EventHandler MyEvent;

#pragma warning disable 67

        public event PropertyChangedEventHandler PropertyChanged;

#pragma warning restore 67

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