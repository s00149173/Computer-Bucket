using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace computerbucket.Models
{
    public class PaymentModal
    {
        [Display(Name="Card Number")]
        [Required]
        public int CardNumber { get; set; }

        [Display(Name = "Expriration Month")]
        [Required]
        public int ExpirationMonth { get; set; }

        [Display(Name = "Expriration Year")]
        [Required]
        public int ExpirationYear { get; set; }

        [Display(Name = "Security Code")]
        [Required]
        public int CardSecurityCode { get; set; }

        [Display(Name="Card Holder Name")]
        [Required]
        public string CardHolder { get; set; }
    }
}