using System.Linq;

namespace LaptopStore.ViewModels
{
    public class OrderListViewModel
    {
        public IQueryable<LaptopStore.Data.Models.Order> userOrders { get; set; }
    }
}
