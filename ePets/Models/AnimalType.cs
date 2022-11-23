using System.ComponentModel.DataAnnotations;

namespace ePets.Models
{
    public class AnimalType
    {
        //Props

        [Key]
        public int AnimalTypeId { get; set; }
        [Required(ErrorMessage = "Please Enter Type Name")]
        public string?  TypeName { get; set; }
    }
}
