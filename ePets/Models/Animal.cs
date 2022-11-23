using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePets.Models
{
    public class Animal
    {
        //Props

        [Key]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Please Uplaod Image")]
        [Display(Name = "Photo Url:")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Animal Name:")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please Enter Age")]
        [Range(1, 300)]
        [Display(Name = "Age:")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        [Range(1, double.MaxValue)]
        [Display(Name = "Price:")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [Display(Name = "Description:")]
        [Range(1, 300)]
        public string? Bio { get; set; }

        
        //Relationships

        [ForeignKey("AnimalTypeId")]
        public int AnimalTypeId { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }



    }
}
