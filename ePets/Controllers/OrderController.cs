using ePets.Data.Cart;
using ePets.Models;
using ePets.Repositories;
using ePets.Repositories.Order_Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ePets.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAnimalService _AnimalContext;
        private readonly ShoppingCart TheShoppingCart;
        private readonly IOrderService _OrderContext;


        public OrderController(IAnimalService _AnimalContext, ShoppingCart _CartContext, IOrderService _OrderContext)
        {
            this._AnimalContext = _AnimalContext;
            TheShoppingCart=_CartContext;
            this._OrderContext = _OrderContext;
        }
        public IActionResult ShoppingCart()
        {
            var items = TheShoppingCart.GetCartItems();
            TheShoppingCart.CartItems = items;

            var respone = new ShoppingCartVM()
            {
                ShoppingCart = TheShoppingCart,
                TotalCartPrice = TheShoppingCart.GetCartTotalSum()
            };
            return View(respone);
        }


        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _OrderContext.GetOrderByUserIdAndRole(userId , userRole);
            return View(orders);
        }

        public IActionResult AddToCart(int id)
        {
            var item = _AnimalContext.GetSpecificAnimal(id);

            if (item != null)
            {
                TheShoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public IActionResult RemoveFromCart(int id)
        {
            var item = _AnimalContext.GetSpecificAnimal(id);

            if (item != null)
            {
                TheShoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = TheShoppingCart.GetCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email);

            await _OrderContext.StoreOrdersAsync(items, userId, email);
            await TheShoppingCart.ClearCartAsync();

            return View("OrderCompleted");
        }
    }

}
