using LaptopStore.Data.Models;
using System.Collections.Generic;

namespace LaptopStore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Laptop> favLaptops { get; set; }
    }
}
