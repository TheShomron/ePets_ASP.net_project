using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePets.Models
{
    public class OrderItem
    {
        [Key]
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int AnimalId { get; set; }

        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

    }
}
