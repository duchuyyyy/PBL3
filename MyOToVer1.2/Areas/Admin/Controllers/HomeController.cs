using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models;
using MyOToVer1._2.Models.DataModels;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly CarModel _carModel;
        private readonly CustomerModel _customerModel;
        private readonly CarImgModel _carImgModel;
        private readonly OwnerModel _ownerModel;

        public HomeController(ApplicationDBContext db)
        {
            _carModel = new CarModel(db);
            _customerModel = new CustomerModel(db);
            _carImgModel= new CarImgModel(db);
            _ownerModel = new OwnerModel(db);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            ViewBag.ListCarWaitToAccept = _carModel.GetListCarWaitAccept();
            foreach(var item in ViewBag.ListCarWaitToAccept)
            {
                var owner = _customerModel.FindCustomerById(item.owner_id);
                var OwnerInfo = _ownerModel.FindOwnerById(item.owner_id);
                ViewBag.OwnerName = owner.Name;
                ViewBag.OwnerContact = owner.Contact;
                ViewBag.OnwerNameBanking = OwnerInfo.owner_name_banking;
                ViewBag.OwnerNumberAccount = OwnerInfo.owner_number_account;
            }
            var img = _carImgModel.FindImageByCar(ViewBag.ListCarWaitToAccept);
            ViewBag.Img = img;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int check)
        {
            return View();
        }

    }
}
