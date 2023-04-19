using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            CarOwnerViewModels obj = TempData["RegisterCar"] as CarOwnerViewModels;

            return View(obj);
        }
        [HttpGet]
        public IActionResult ConfirmCarOwner(CarOwnerViewModels obj)
        {
            return View(obj);
        }
    }
}
