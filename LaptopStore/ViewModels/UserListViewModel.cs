using System.Linq;

namespace LaptopStore.ViewModels
{
    public class UserListViewModel
    {
        public IQueryable<LaptopStore.Data.Models.User> allUsers { get; set; }
    }
}