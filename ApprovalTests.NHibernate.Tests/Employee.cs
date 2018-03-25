namespace ApprovalTests.NHibernate.Tests
{
    using System.Collections.Generic;

    public class Employee
    {
        public Employee()
        {
            Employee1 = new HashSet<Employee>();
            Events = new HashSet<Event>();
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Boss { get; set; }
        public int? Company { get; set; }
        public virtual Company Company1 { get; set; }
        public virtual ICollection<Employee> Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}