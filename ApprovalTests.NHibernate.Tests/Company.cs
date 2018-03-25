namespace ApprovalTests.NHibernate.Tests
{
    using System.Collections.Generic;

    public class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Website { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}