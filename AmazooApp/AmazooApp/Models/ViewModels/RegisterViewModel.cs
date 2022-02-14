using System.ComponentModel.DataAnnotations;

namespace AmazooApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100,ErrorMessage ="the {0} must atleast {2} x-acteers long",MinimumLength =6)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="Role")]
        public string Role { get; set; }

    }
}
