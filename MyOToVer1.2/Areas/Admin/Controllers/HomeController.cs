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
            ViewBag.Img6 = _carImgModel.FindImageByCarOwnCus(ViewBag.ListCarWaitToAccept);

            ViewBag.ListCarWaitToUpdate = _carModel.GetListCarWaitUpdate();
            ViewBag.Img1 = _carImgModel.FindImageByCarOwnCus(ViewBag.ListCarWaitToUpdate);

            ViewBag.Img = _carImgModel.FindImageByCarOwnCus(ViewBag.ListCarWaitToAccept);
            ViewBag.IdentityPhotos = _ownerIdentityPhotoModel.GetPhotoByOwnerIdOwnCus(ViewBag.ListCarWaitToAccept);
           

            ViewBag.ListCarRenting = _carCustomerModel.GetListCarRenting();
            ViewBag.Img2 = _carImgModel.FindImageByCarCus(ViewBag.ListCarRenting);

            ViewBag.ListCarPauseToRent = _carCustomerModel.GetListCarPauseToRent();
            ViewBag.Img3 = _carImgModel.FindImageByCarCus(ViewBag.ListCarPauseToRent);

            ViewBag.ListAccountBeLocked = _infoOwnerModel.GetListOwnerBeLocked();
            
            
            ViewBag.ListCarRentalBeReported = _carRentalBeReportedModel.GetListCarRentalBeReported();

            ViewBag.ListCustomerRefund = _carRentalBeReportedModel.GetListCustomerRefund();
            return View();
        }

        [HttpPost]
        public IActionResult Index(int check, int carId, int checkupdate, int carIdUpdate, int ownerid)
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
                if (checkupdate == 1)
                {
                    var car = _carModel.FindCarById(carIdUpdate);
                    car.update_status = 1;
                    car.is_update = false;
                    car.update_car_price = car.car_price;
                    car.update_car_rule = car.car_rule;
                    car.update_car_description = car.car_description;
                    car.update_car_rule = car.car_rule;
                    _carModel.UpdateCar(car);
                    return RedirectToAction("Index");
                }
                else if (checkupdate == 2)
                {
                    var car = _carModel.FindCarById(carIdUpdate);
                    car.update_status = 2;
                    car.is_update = false;
                    car.car_price = car.update_car_price;
                    car.car_rule = car.update_car_rule;
                    car.car_description = car.update_car_description;
                    car.car_rule = car.update_car_rule;
                    _carModel.UpdateCar(car);
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
