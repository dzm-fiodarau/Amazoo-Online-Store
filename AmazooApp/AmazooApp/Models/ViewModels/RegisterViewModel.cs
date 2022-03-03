using System.ComponentModel.DataAnnotations;

namespace AmazooApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100,ErrorMessage ="the password {0} must atleast be {2} characters long",MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="Role")]
        public string Role { get; set; }

        [Display(Name ="City")]
        [Required]
        public string City { get; set; }
        [Display(Name = "Province")]
        [Required]
        public string Province { get; set; }
        [Display(Name = "Zip")]
   
        public string Zipcode { get; set; }


        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

    }
}
