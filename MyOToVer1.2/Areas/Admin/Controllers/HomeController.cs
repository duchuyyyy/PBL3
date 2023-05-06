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

        public HomeController(ApplicationDBContext db)
        {
            _carModel = new CarModel(db);
            _customerModel = new CustomerModel(db);
            _carImgModel= new CarImgModel(db);
            _ownerModel = new OwnerModel(db);
            _carCustomerModel = new CarCustomerModel(db);
            _ownerIdentityPhotoModel= new OwnerIdentityPhotoModel(db);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            ViewBag.ListCarWaitToAccept = _carModel.GetListCarWaitAccept();
            foreach(var item in ViewBag.ListCarWaitToAccept)
            {
                var owner = _customerModel.FindCustomerById(item.owner_id);
                var OwnerInfo = _ownerModel.FindOwnerById(item.owner_id);
                var Img = _carImgModel.BannerImg(item.car_id);
                ViewBag.OwnerName = owner.Name;
                ViewBag.OwnerContact = owner.Contact;
                ViewBag.OnwerNameBanking = OwnerInfo.owner_name_banking;
                ViewBag.OwnerNumberAccount = OwnerInfo.owner_number_account;
                ViewBag.Img= Img.name_img;
            }
            ViewBag.ListCarWaitToUpdate = _carModel.GetListCarWaitUpdate();
            foreach(var item in ViewBag.ListCarWaitToUpdate)
            {
                var owner = _customerModel.FindCustomerById(item.owner_id);
                ViewBag.OwnerName1 = owner.Name;
                ViewBag.OwnerContact1 = owner.Contact;
                var Img = _carImgModel.BannerImg(item.car_id);
                ViewBag.Img1 = Img.name_img;
            }
           
            var img = _carImgModel.FindImageByCar(ViewBag.ListCarWaitToAccept);
            ViewBag.Img = img;
            var ownerPhotos = _ownerIdentityPhotoModel.GetPhotoByOwnerId(ViewBag.ListCarWaitToAccept);
            ViewBag.IdentityPhotos = ownerPhotos;

            ViewBag.ListCarRenting = _carCustomerModel.GetListCarRenting();
            ViewBag.ListCarPauseToRent = _carCustomerModel.GetListCarPauseToRent();
            return View();
        }

        [HttpPost]
        public IActionResult Index(int check, int carId, int checkupdate, int carIdUpdate)
        {
            try
            {
                if(check == 1)
                {
                    var car = _carModel.FindCarById(carId);
                    car.accept_status = 1;
                    _carModel.UpdateCar(car);
                    return RedirectToAction("Index");
                }
                else if(check == 2)
                {
                    var car = _carModel.FindCarById(carId);
                    car.accept_status = 2;
                    car.is_accept= true;
                   _carModel.UpdateCar(car);
                    return RedirectToAction("Index");
                }
                if(checkupdate==1)
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
                else if(checkupdate==2) 
                {
                    var car = _carModel.FindCarById(carIdUpdate);
                    car.update_status = 2;
                    car.is_update= false;
                    car.car_price = car.update_car_price;
                    car.car_rule= car.update_car_rule;
                    car.car_description=car.update_car_description;
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
