using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using LaptopStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LaptopStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ILaptops _laptopRepository;
        private readonly Cart _cart;

        public CartController(ILaptops laptopRepository, Cart cart)
        {
            _laptopRepository = laptopRepository;
            _cart = cart;
        }

        public ViewResult Index()
        {
            var items = _cart.GetCartItems();
            _cart.ListCartItems = items;
            _cart.itemsCount = items.Count();

            var obj = new CartViewModel
            {
                cart = _cart
            };

            return View(obj);
        }

        public RedirectToActionResult AddToCart(long id)
        {
            var item = _laptopRepository.GetAll().Single(i => i.id == id);
            if(item != null)
            {
                _cart.AddToCart(item);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult DeleteFromCart(long id)
        {
            var item = _cart.GetCartItems().Single(i => i.id == id);
            if (item != null)
            {
                _cart.DeleteFromCart(item);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult Clear(string cartId)
        {
            var items = _cart.GetCartItems().Where(i => i.CartId == cartId);
            foreach (var item in items)
            {
                _cart.DeleteFromCart(item);
            }

            return RedirectToAction("Index");
        }

    }
}
