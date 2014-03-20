using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace computerbucket.Models
{
    public class Status
    {
        public int OrderID { get; set; }
        [Required(ErrorMessage = "Need value for email")]
        [DataType(DataType.EmailAddress)]
        public string StatusField { get; set; }
        public string Name { get; set; }
    }
}