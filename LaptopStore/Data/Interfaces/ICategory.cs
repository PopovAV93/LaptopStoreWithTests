using LaptopStore.Data.Models;
using System.Linq;

namespace LaptopStore.Data.Interfaces
{
    public interface ICategory : IBaseRepository<Category>
    {
        Category GetCategoryByLaptop(Laptop laptop);
    }
}
