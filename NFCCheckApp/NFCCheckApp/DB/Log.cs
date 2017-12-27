using System;
using System.Collections.Generic;

namespace NFCCheckApp
{
    public partial class Log
    {
        public int LogId { get; set; }
        public int BindigId { get; set; }
        public int GateId { get; set; }
        public DateTime Time { get; set; }
        public string Event { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }

        public Binding Bindig { get; set; }
        public Gate Gate { get; set; }
    }
}
