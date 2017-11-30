using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarraLeather.Models
{
    public class UserLoging
    {
        public string nombre { get; set; }
        public string clave { get; set; }
        public bool remeber { get; set; }
    }
    public class EstatusLog
    {
        public EstatusLog()
        {
            success = true;
        }
        public bool error { get; set; }
        public bool success { get; set; }
        public string errorMsg { get; set; }
    }
}