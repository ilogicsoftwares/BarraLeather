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
    using System;
    using System.Collections.Generic;
    
    public partial class productos
    {
        public productos()
        {
            this.cart = new HashSet<cart>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<float> precio { get; set; }
        public string descripcion { get; set; }
        public Nullable<float> inventario { get; set; }
        public string foto { get; set; }
        public Nullable<int> status { get; set; }
        public string category { get; set; }
        public Nullable<int> categoryid { get; set; }
    
        public virtual ICollection<cart> cart { get; set; }
    }
}
