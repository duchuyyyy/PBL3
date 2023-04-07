using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models.DataModels;

namespace MyOToVer1._2.Controllers
{
    public class OwnerController : Controller
    {
        private readonly CustomerModel _customerModel;
        private readonly OwnerModel _ownerModel;

        public OwnerController(ApplicationDBContext db)
        {
            _customerModel = new CustomerModel(db);
            _ownerModel = new OwnerModel(db);
        }

        public IActionResult OwnerOrder()
        {
            ViewBag.Name = HomeController.username;
            return View();
        }
    }
}
