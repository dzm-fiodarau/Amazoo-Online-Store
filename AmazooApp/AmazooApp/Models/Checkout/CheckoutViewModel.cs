using System.ComponentModel.DataAnnotations;
using AmazooApp.Utility;

namespace AmazooApp.Models.Checkout
{
    public class CheckoutViewModel {


        [Key]
        public bool Addon { get; set; }
        public string chkAddon { get; set; }
        public bool flag = false;
        public bool useAlternateAddress { get; set; }
        

        public ApplicationUser currentUser { get; set; }
        public ApplicationUser tempUser { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Province")]
        public string Province { get; set; }


        [Display(Name = "Zip")]
        public string Zipcode { get; set; }






        public CreditCard cc { get; set; }


        [Required]
        [Display(Name = "Credit Card Number")]
        public string cardNumber { get; set; }


        [Required]
        [Display(Name = "Card Holder Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Expiration Month")]
        public string expMonth { get; set; }
        [Required]
        [Display(Name = "Expiration Year")]
        public string expYear { get; set; }

        [Required]
        [Display(Name = "CCV")]
        public string ccv { get; set; }

    }
}

