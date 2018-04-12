namespace ApprovalTests.NHibernate.Tests
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Employee { get; set; }
        public string Status { get; set; }
        public virtual Employee Employee1 { get; set; }
    }
}