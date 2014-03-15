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

        [Required(ErrorMessage = "Need value for Adress"), StringLength(32, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Need value for City"), StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string City { get; set; }

        [Required(ErrorMessage = "Need value for Region"), StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Region { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Need value for Postal Code"), StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 4)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Need value for Coutry"), StringLength(30, ErrorMessage = "Must have at least {2} characters..", MinimumLength = 2)]
        public string Country { get; set; }

        [Display(Name="Phone Number")]
        [Range(typeof(decimal), "0", "9999999999999999", ErrorMessage = "Enter a valid phone number!")]
        public string Phone { get; set; }

        [Display(Name="Email Adress")]
        [Required(ErrorMessage = "Need value for email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}