using System.Collections.Generic;

namespace LaptopStore.Data.Models
{
    public class Category
    {
        public long id { get; set; }
        public string categoryName { get; set; }
        public string desc { get; set; }
        public List<Laptop> laptops { get; set; }
    }
}
