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
        [Required(ErrorMessage = "Card Number is a required field!")]
        [Range(typeof(long), "0000000000000000", "9999999999999999", ErrorMessage = "The Card number is invalid!!")]
        public long CardNumber { get; set; }

        [Display(Name = "Expriration Month")]
        [Required(ErrorMessage = "Card Expriration Month is a required field!")]
        public int ExpirationMonth { get; set; }

        [Display(Name = "Expriration Year")]
        [Required(ErrorMessage = "Card Expriration Year is a required field!")]
        public int ExpirationYear { get; set; }

        [Display(Name = "Security Code")]
        [Required(ErrorMessage = "Card Security Code is a required field!")]
        [Range(typeof(int), "000", "999", ErrorMessage = "The Security is invalid!!")]
        public int CardSecurityCode { get; set; }

        [Display(Name = "Card Holder Name"), StringLength(30, ErrorMessage = "The Card Holder Name is invalid!!", MinimumLength = 6)]
        [Required(ErrorMessage = "Card Holder Name is a required field!")]
        public string CardHolder { get; set; }
    }
}