//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace computerbucket
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderMotherboard_tbl
    {
        public int OM_id { get; set; }
        public int order_id { get; set; }
        public int motherboard_id { get; set; }
    
        public virtual Motherboard_tbl Motherboard_tbl { get; set; }
        public virtual Orders_tbl Orders_tbl { get; set; }
    }
}