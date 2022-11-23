using ePets.Models;

namespace ePets.Repositories.Order_Repository
{
    public interface IOrderService
    {
        Task StoreOrdersAsync(List<ShoppingCartItem> items , string UserId , string Email);
        Task<List<Order>> GetOrderByUserIdAndRole(string UserId, string UserRole);
    }
}
