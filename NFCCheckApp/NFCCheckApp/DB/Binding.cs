using System;
using System.Collections.Generic;

namespace NFCCheckApp
{
    public partial class Binding
    {
        public Binding()
        {
            Log = new HashSet<Log>();
        }

        public int BindingId { get; set; }
        public int ToolId { get; set; }
        public int EmployeeId { get; set; }
        public bool Pull { get; set; }

        public Employee Employee { get; set; }
        public Tool Tool { get; set; }
        public ICollection<Log> Log { get; set; }
    }
}
