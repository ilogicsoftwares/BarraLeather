using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarraLeather.Logic
{
    public static class logic
    {
       
        public static users ActiveUser()
        {
            tiendaEntities db = new tiendaEntities();
            var id=int.Parse(HttpContext.Current.User.Identity.Name);
            users actualuser=db.users.FirstOrDefault(x => x.id == id);
            return actualuser;

        }
    }
}