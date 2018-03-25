namespace ApprovalTests.NHibernate.Tests
{
    using System;

    public class Event
    {
        public int Id { get; set; }
        public Nullable<int> Employee { get; set; }
        public string EventTitle { get; set; }
        public string Details { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}