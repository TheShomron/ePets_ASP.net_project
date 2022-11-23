using System.ComponentModel.DataAnnotations;

namespace ePets.Models
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; }

        [Display(Name ="Email Address")]
        [Required(ErrorMessage = "Email Is Required")]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Do Not Match")]
        public String ConfirmPassword { get; set; }
    }
}
