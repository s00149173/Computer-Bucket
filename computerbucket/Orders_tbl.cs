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
    
    public partial class Orders_tbl
    {
        public Orders_tbl()
        {
            this.CustumerOrders = new HashSet<CustumerOrder>();
            this.OrderMotherboard_tbl = new HashSet<OrderMotherboard_tbl>();
            this.OrderPart_tbl = new HashSet<OrderPart_tbl>();
        }
    
        public int order_id { get; set; }
        public Nullable<System.DateTime> date_ordered { get; set; }
    
        public virtual ICollection<CustumerOrder> CustumerOrders { get; set; }
        public virtual ICollection<OrderMotherboard_tbl> OrderMotherboard_tbl { get; set; }
        public virtual ICollection<OrderPart_tbl> OrderPart_tbl { get; set; }
    }
}
