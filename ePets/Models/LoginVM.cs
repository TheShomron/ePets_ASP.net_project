using System.ComponentModel.DataAnnotations;

namespace ePets.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
