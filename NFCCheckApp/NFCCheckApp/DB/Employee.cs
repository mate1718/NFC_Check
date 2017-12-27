using System;
using System.Collections.Generic;

namespace NFCCheckApp
{
    public partial class Employee
    {
        public Employee()
        {
            Binding = new HashSet<Binding>();
        }

        public int EmployeeId { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Rank { get; set; }
        public int BadgeId { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
        public bool It { get; set; }

        public ICollection<Binding> Binding { get; set; }
    }
}
