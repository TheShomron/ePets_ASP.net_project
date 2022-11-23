using ePets.Models;
using Microsoft.EntityFrameworkCore;

namespace ePets.Data.Cart
{
    public class ShoppingCart
    {
        MyAppDbContext _context { get; set; }

        public string CartId { get; set; }

        public List<ShoppingCartItem> CartItems { get; set; }

        public ShoppingCart(MyAppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider servics)
        {
            ISession session = servics.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = servics.GetService<MyAppDbContext>();

            string cartId = session.GetString("CartId")??Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { CartId = cartId };
        }
        public void AddItemToCart(Animal animal)
        {
            var ShoppingCartItem = _context.ShoppingCartItems.
                FirstOrDefault(n => n.Animal.AnimalId == animal.AnimalId &&
                n.ShoppingCartId == CartId);

            if(ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = CartId,
                    Animal = animal,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Animal animal)
        {
            var ShoppingCartItem = _context.ShoppingCartItems.
                          FirstOrDefault(n => n.Animal.AnimalId == animal.AnimalId &&
                          n.ShoppingCartId == CartId);

            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Amount>1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);
                }
            }  
            _context.SaveChanges();
        }
        public List<ShoppingCartItem> GetCartItems()
        {
            return CartItems ?? (CartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == CartId).Include(n => n.Animal).ToList());
        }
        public double GetCartTotalSum()
        {
            return _context.ShoppingCartItems. Where(n => n.ShoppingCartId == CartId).Select(n => n.Animal.Price*n.Amount).Sum();
        }

        public async Task ClearCartAsync()
        {
            var items =await _context.ShoppingCartItems.Where(n => n.ShoppingCartId== CartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
