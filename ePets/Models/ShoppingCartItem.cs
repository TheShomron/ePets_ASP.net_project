namespace ePets.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public virtual Animal? Animal { get; set; }
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
