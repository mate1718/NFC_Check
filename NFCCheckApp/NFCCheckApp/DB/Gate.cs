using System;
using System.Collections.Generic;

namespace NFCCheckApp
{
    public partial class Gate
    {
        public Gate()
        {
            Log = new HashSet<Log>();
        }

        public int GateId { get; set; }
        public int GateNumber { get; set; }
        public string GateInstruction { get; set; }

        public ICollection<Log> Log { get; set; }
    }
}
