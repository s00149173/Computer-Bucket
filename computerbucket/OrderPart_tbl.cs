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
    
    public partial class OrderPart_tbl
    {
        public int order_id { get; set; }
        public int part_id { get; set; }
    
        public virtual Orders_tbl Orders_tbl { get; set; }
    }
}
