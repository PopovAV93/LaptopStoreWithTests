using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaptopStore.Data.Models
{
    public class Cart
    {
        private readonly AppDBContent appDBContent;
        public Cart(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public string CartId { get; set; }
        public List<CartItem> ListCartItems { get; set; }
        public int itemsCount { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; //new session creating
            var context = services.GetService<AppDBContent>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new Cart(context) { CartId = cartId };
        }

        public void AddToCart(Laptop laptop)
        {
            this.appDBContent.CartItems.Add(new CartItem { CartId = CartId, laptop = laptop, price = laptop.price });
            this.appDBContent.SaveChanges();
        }

        public void DeleteFromCart(CartItem cartItem)
        {
            this.appDBContent.CartItems.Remove(cartItem);
            this.appDBContent.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return this.appDBContent.CartItems.Where(c => c.CartId == CartId).Include(s => s.laptop).ToList();
        }
    }
}
