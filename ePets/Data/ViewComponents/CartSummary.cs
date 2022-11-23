using ePets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ePets.Data.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly ShoppingCart TheShoppingCart;

        public CartSummary(ShoppingCart context)
        {
            TheShoppingCart = context;
        }



        public IViewComponentResult Invoke()
        {
            var item = TheShoppingCart.GetCartItems();
            return View(item.Count());
        }
    }
}
