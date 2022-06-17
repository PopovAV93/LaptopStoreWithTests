using System.Threading.Tasks;
using LaptopStore.ViewModels;
using LaptopStore.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LaptopStore.Data.Models;
using System.Linq;

namespace LaptopStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfiles _profiles;

        public ProfileController(IProfiles profiles)
        {
            _profiles = profiles;
        }

        
        public async Task<IActionResult> ProfileInfo()
        {
            var email = User.Identity?.Name;
            var response = await _profiles.GetProfile(email);
            if (response.StatusCode == Data.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        public IActionResult Save() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Save(Profile model)
        {
            if (ModelState.IsValid)
            {
                if (model.id != 0)
                {
                    await _profiles.Update(model);
                }
                return RedirectToAction("ProfileInfo");
            }
            return RedirectToAction("ProfileInfo");
        }
    }
}