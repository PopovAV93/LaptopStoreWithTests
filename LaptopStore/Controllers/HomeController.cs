using LaptopStore.Data.Interfaces;
using LaptopStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILaptops _laptopRepository;

        public HomeController(ILaptops laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }
        public ViewResult Index()
        {
            var homeLaptops = new HomeViewModel { favLaptops = _laptopRepository.GetFavLaptops() };
            return View(homeLaptops);
        }
    }
}
