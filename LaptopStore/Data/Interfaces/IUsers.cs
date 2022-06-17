using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopStore.Data.Models;
using LaptopStore.Data.Response;
using LaptopStore.ViewModels;

namespace LaptopStore.Data.Interfaces
{
    public interface IUsers : IBaseRepository<User>
    {
        public Task Delete(long id);

        public IQueryable<Order> UserOrders(string email);
    }
}