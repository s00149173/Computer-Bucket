using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace computerbucket.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }       
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}