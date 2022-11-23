using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePets.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public virtual List<OrderItem> OrderItemsList { get; set; }

    }
}
