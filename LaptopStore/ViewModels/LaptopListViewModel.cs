using LaptopStore.Data.Models;
using System.Linq;

namespace LaptopStore.ViewModels
{
    public class LaptopListViewModel
    {
        public IQueryable<Laptop> allLaptops { get; set; }
        public string currCategory { get; set; }
    }
}
