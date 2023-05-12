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
        private readonly CarCustomerModel _carCustomerModel;
        private readonly OwnerIdentityPhotoModel _ownerIdentityPhotoModel;
        private readonly CarRentalBeReportedModel _carRentalBeReportedModel;
        private readonly InfoOwnerModel _infoOwnerModel;

        public HomeController(ApplicationDBContext db)
        {
            _carModel = new CarModel(db);
            _customerModel = new CustomerModel(db);
            _carImgModel= new CarImgModel(db);
            _ownerModel = new OwnerModel(db);
            _carCustomerModel = new CarCustomerModel(db);
            _ownerIdentityPhotoModel= new OwnerIdentityPhotoModel(db);
            _carRentalBeReportedModel = new CarRentalBeReportedModel(db);
            _infoOwnerModel= new InfoOwnerModel(db);
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
            var ownerPhotos = _ownerIdentityPhotoModel.GetPhotoByOwnerId(ViewBag.ListCarWaitToAccept);
            ViewBag.IdentityPhotos = ownerPhotos;

            ViewBag.ListCarRenting = _carCustomerModel.GetListCarRenting();
            ViewBag.ListCarPauseToRent = _carCustomerModel.GetListCarPauseToRent();
            ViewBag.ListCarRentalBeReported = _carRentalBeReportedModel.GetListCarRentalBeReported();
            ViewBag.ListAccountBeLocked = _infoOwnerModel.GetListOwnerBeLocked();
            ViewBag.ListCustomerRefund = _carRentalBeReportedModel.GetListCustomerRefund();
            return View();
        }

        [HttpPost]
        public IActionResult Index(int check, int carId, int ownerid)
        {
            try
            {
                if(check == 1) // Tu choi xe
                {
                    var car = _carModel.FindCarById(carId);
                    car.accept_status = 1;
                    _carModel.UpdateCar(car);
                    return RedirectToAction("Index");
                }
                else if(check == 2) // Phe duyet xe
                {
                    var car = _carModel.FindCarById(carId);
                    car.accept_status = 2;
                    car.is_accept= true;
                   _carModel.UpdateCar(car);

                    var owner = _ownerModel.FindOwnerById(car.owner_id);
                    owner.owner_status = 2;
                    _ownerModel.UpdateOwner(owner);
                    return RedirectToAction("Index");
                }
                else if(check == 3) // Khoa tai khoan chu xe
                {
                    var owner = _ownerModel.FindOwnerById(ownerid);
                    owner.owner_status = 1;
                    _ownerModel.UpdateOwner(owner);
                    return RedirectToAction("Index");
                }
                else if(check == 4) // Mo khoa tai khoan
                {
                    var owner = _ownerModel.FindOwnerById(ownerid);
                    owner.owner_status = 2;
                    _ownerModel.UpdateOwner(owner);
                    return RedirectToAction("Index");
                }
                return View();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
