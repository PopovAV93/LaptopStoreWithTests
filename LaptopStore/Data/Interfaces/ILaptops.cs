using LaptopStore.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace LaptopStore.Data.Interfaces
{
    public interface ILaptops : IBaseRepository<Laptop>
    {
        IQueryable<Laptop> GetFavLaptops();
        Laptop GetObjectLaptop(long laptopId);
        IQueryable<Laptop> GetLaptopsByCategory(Category category);
    }
}
