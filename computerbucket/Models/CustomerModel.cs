using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace computerbucket.Models
{

    public class CustomerModel
    {
        [Display(Name = "Customer Name"), StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "Need value for Customer Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Need value for Adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Need value for City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Need value for Region")]
        public string Region { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Need value for Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Need value for Coutry")]
        public string Country { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Need value for email")]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}