//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarraLeather
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class cart
    {
        public int id { get; set; }
        [JsonIgnore]
        public int userid { get; set; }
        public int prodid { get; set; }
        public int cantidad { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual productos productos { get; set; }
        [JsonIgnore]
        public virtual users users { get; set; }
    }
}
