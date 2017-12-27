using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFCCheckApp
{
    class DataBase
    {
        public void ErrorSave(int GateId,string comment)
        {   using (var db = new NFC_Check_DBContext())
            {
                Log log = new Log();
                log.BindigId=-1;
                log.GateId = GateId;
                log.Time = DateTime.Now;
                log.Event = "Error";
                log.Comment = comment;
                db.Log.Add(log);
                db.SaveChanges();
            }
        }
        public void Save(int GateID,int bindingID,bool active)
        {
            using (var db = new NFC_Check_DBContext())
            {
                Log log = new Log();
                log.BindigId = bindingID;
                log.GateId = GateID;
                log.Time = DateTime.Now;
                log.Event = active ? "Login" : "Logout";
                db.Log.Add(log);
                db.SaveChanges();
            }
        }
    }
}
