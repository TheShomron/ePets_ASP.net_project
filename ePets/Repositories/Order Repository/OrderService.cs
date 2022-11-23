using ePets.Data;
using ePets.Models;
using Microsoft.EntityFrameworkCore;

namespace ePets.Repositories.Order_Repository
{
    public class OrderService : IOrderService
    {
        private readonly MyAppDbContext _context;

        public OrderService(MyAppDbContext context)
        {
            _context=context;
        }

        public async Task<List<Order>> GetOrderByUserIdAndRole(string UserId ,string UserRole)
        {
            var Orders = await _context.Orders.Include(n => n.OrderItemsList).
                ThenInclude(n => n.Animal).Include(n=> n.User).ToListAsync();

            if(UserRole != "Admin")
            {
                Orders = Orders.Where(n => n.UserId == UserId).ToList();
            }
            return Orders;
        }

        public async Task StoreOrdersAsync(List<ShoppingCartItem> items, string UserId, string Email)
        {
            var Order = new Order()
            {
                UserId = UserId,
                Email=Email
            };
            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var OrderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    AnimalId = item.Animal.AnimalId,
                    OrderId = Order.OrderId,
                    Price = item.Animal.Price
                };
                await _context.OrderItems.AddAsync(OrderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
