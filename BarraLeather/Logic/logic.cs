using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BarraLeather.Logic
{
    public static class logic
    {
       
        public static users ActiveUser()
        {
            if (HttpContext.Current.User.Identity.Name == string.Empty)
            {
                return null;
            }
                tiendaEntities db = new tiendaEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var id=int.Parse(HttpContext.Current.User.Identity.Name);
            users actualuser=db.users.FirstOrDefault(x => x.id == id);
            return actualuser;

        }
      
    }
}