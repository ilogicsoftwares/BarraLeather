using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarraLeather.Models
{
    public class models
    {
        public class user
        {
            public string nombre { get; set; }
            public string clave { get; set; }
            public string email { get; set; }
        }
        public class objectId {

            public int id { get; set; }

        }

        public class Contact
        {
            public string nombre { get; set; }
            public string email { get; set; }
            public string msg { get; set; }

        }

    }
}