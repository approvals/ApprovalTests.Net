namespace ApprovalTests.NHibernate.Tests
{
    public class Event
    {
        public int Id { get; set; }
        public int? Employee { get; set; }
        public string EventTitle { get; set; }
        public string Details { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}