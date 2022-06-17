using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopStore.Data.Mocks
{
    public class MockLaptopCategories
    {
        public IEnumerable<Category> AllCategories 
        {
            get
            {
                return new List<Category>
                {
                    new Category {id = 1, categoryName = "Ultrathin", desc = "Ultrathin laptops"},
                    new Category {id = 2, categoryName = "Transformer", desc = "Touch screen laptops, tablet/laptop hybrid"},
                    new Category {id = 3, categoryName = "Office", desc = "Laptops for study and work"},
                    new Category {id = 4, categoryName = "Gaming", desc = "Laptops with powerful GPUs and CPUs"}
                };
            }
        }
    }
}
