﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class computerbucketEntities : DbContext
    {
        public computerbucketEntities()
            : base("name=computerbucketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Category_tbl> Category_tbl { get; set; }
        public DbSet<Comparison_tbl> Comparison_tbl { get; set; }
        public DbSet<Custumer_tbl> Custumer_tbl { get; set; }
        public DbSet<CustumerOrder> CustumerOrders { get; set; }
        public DbSet<Motherboard_tbl> Motherboard_tbl { get; set; }
        public DbSet<OrderMotherboard_tbl> OrderMotherboard_tbl { get; set; }
        public DbSet<OrderPart_tbl> OrderPart_tbl { get; set; }
        public DbSet<Orders_tbl> Orders_tbl { get; set; }
        public DbSet<Parts_tbl> Parts_tbl { get; set; }
    }
}
