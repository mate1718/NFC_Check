using System;
using System.Collections.Generic;

namespace NFCCheckApp
{
    public partial class Tool
    {
        public Tool()
        {
            Binding = new HashSet<Binding>();
        }

        public int ToolId { get; set; }
        public int SerialNumber { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public int NfcId { get; set; }
        public bool Active { get; set; }
        public bool Archive { get; set; }

        public ICollection<Binding> Binding { get; set; }
    }
}
