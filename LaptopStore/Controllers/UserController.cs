using System.Linq;
using System.Threading.Tasks;
using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using LaptopStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsers _users;

        public UserController(IUsers users)
        {
            _users = users;
        }
        
        public PartialViewResult GetUsers()
        {
            var userList = GetAllUsers();

            return PartialView(userList);
        }

        public PartialViewResult UserOrders()
        {
            var email = User.Identity?.Name;
            if (email != null)
            {
                var orders = GetUserOrders(email);
                return PartialView(orders);
            }

            return PartialView();
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            //User user = _users.GetAll().Single(x => x.id == id);
            await _users.Delete(id);
            
            return RedirectToAction("ProfileInfo", "Profile");
        }

        private OrderListViewModel GetUserOrders(string email)
        {
            var orders = new OrderListViewModel()
            {
                userOrders = _users.UserOrders(email)
            };

            return orders;
        }

        private UserListViewModel GetAllUsers()
        {
            var userList = new UserListViewModel()
            {
                allUsers = _users.GetAll().OrderBy(i => i.id)
            };

            return userList;
        }

    }
}