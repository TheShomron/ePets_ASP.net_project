using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePets.Models
{
    public class Comment
    {
        //props

        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Please Enter Content With At least 1 Charactar")]
        [Range(2, 300)]
        public string Content { get; set; } = null!;

        //Relationships
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
       public virtual Animal animal { get; set; }





    }
}
