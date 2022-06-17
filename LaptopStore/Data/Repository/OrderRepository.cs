using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopStore.Data.Repository
{
    public class OrderRepository : IOrders
    {
        private readonly AppDBContent _db;
        private readonly Cart _cart;

        public OrderRepository(AppDBContent db, Cart cart)
        {
            this._db = db;
            this._cart = cart;
        }

        public async Task Create(Order order)
        {   
            order.orderTime = DateTime.Now;
            
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            var items = _cart.ListCartItems;

            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    laptopId = el.laptop.id,
                    orderId = order.id,
                    price = el.laptop.price
                };
                await _db.OrderDetails.AddAsync(orderDetail);
            }
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }

        public Task<Order> Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
