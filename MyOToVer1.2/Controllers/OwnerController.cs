using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models;
using MyOToVer1._2.Models.DataModels;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Controllers
{
    public class OwnerController : Controller
    {
        private readonly CustomerModel _customerModel;
        private readonly OwnerModel _ownerModel;
        private readonly CarModel _carModel;
        private readonly CarRentalModel _carRentalModel;
        private readonly CarRentalCarOwnModel _carRentalCarOwnModel;
        private readonly CarImgModel _carImgModel;

        public OwnerController(ApplicationDBContext db)
        {
            _customerModel = new CustomerModel(db);
            _ownerModel = new OwnerModel(db);
            _carModel = new CarModel(db);
            _carRentalModel = new CarRentalModel(db);
            _carRentalCarOwnModel = new CarRentalCarOwnModel(db);
            _carImgModel = new CarImgModel(db); 
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public IActionResult OwnerOrder()
        {
            ViewBag.Name = AccountController.username;

            var listNotConfirm = _carRentalCarOwnModel.GetListNotConfirm(AccountController.id);

            var listConfirmed = _carRentalCarOwnModel.GetListConfirmed(AccountController.id);

            var listWaitToHandOverCar = _carRentalCarOwnModel.GetListWaiToHandOverCar(AccountController.id);

            var listOrderCompleted = _carRentalCarOwnModel.GetListOrderCompleted(AccountController.id);

            ViewBag.Car = listNotConfirm;

            ViewBag.Car2 = listConfirmed;

            ViewBag.Car3 = listWaitToHandOverCar;

            ViewBag.Car4 = listOrderCompleted;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult OwnerOrder(int count, int id, DateTime rentalDateTime)
        {

            ViewBag.Name = AccountController.username;
            try
            {
                if (count == 1)
                {
                    var carRental = _carRentalModel.FindCarRentalById(id);
                    carRental.deposit_status = 2;
                    carRental.rental_status = 2;
                    _carRentalModel.UpdateCarRental(carRental);
                }
                else if (count == 2)
                {
                    var carRental = _carRentalModel.FindCarRentalById(id);
                    DateTime rentalDate = rentalDateTime.Date;
                    //if (rentalDate >= DateTime.Today)
                    //{
                    //    return Content("Khong duoc phep giao xe truoc ngay thue!");
                    //}
                    //else
                    //{
                    //    carRental.rental_status = 3;
                    //    _carRentalModel.UpdateCarRental(carRental);
                    //}

                    carRental.rental_status = 3;
                    _carRentalModel.UpdateCarRental(carRental);
                }
                else if (count == 3)
                {
                    var carRental = _carRentalModel.FindCarRentalById(id);
                    var owner = _ownerModel.FindOwnerById(AccountController.id);
                    if (owner != null)
                    {
                        owner.owner_revenue += carRental.total_price;
                    }
                    carRental.rental_status = 4;
                    _ownerModel.UpdateOwner(owner);
                    _carRentalModel.UpdateCarRental(carRental);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var listNotConfirm = _carRentalCarOwnModel.GetListNotConfirm(AccountController.id);

            var listConfirmed = _carRentalCarOwnModel.GetListConfirmed(AccountController.id);

            var listWaitToHandOverCar = _carRentalCarOwnModel.GetListWaiToHandOverCar(AccountController.id);

            var listOrderCompleted = _carRentalCarOwnModel.GetListOrderCompleted(AccountController.id);

            ViewBag.Car = listNotConfirm;

            ViewBag.Car2 = listConfirmed;

            ViewBag.Car3 = listWaitToHandOverCar;

            ViewBag.Car4 = listOrderCompleted;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public IActionResult MyCar()
        {
            ViewBag.Name = AccountController.username;
            var listCar = _carModel.GetAllCarsByOwnerId(AccountController.id);
            ViewBag.Car = listCar;
            var Img= _carImgModel.FindImageByCar(listCar);
            ViewBag.Img = Img;
            var owner = _ownerModel.FindOwnerById(AccountController.id);
            ViewBag.Revenue = owner.owner_revenue;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult MyCar(int check, int  carid ,int check2, string update_car_description, string update_street_address, string update_car_rule, int? update_car_price)
        {
            try
            {
                ViewBag.Name = AccountController.username;

                var listCar = _carModel.GetAllCarsByOwnerId(AccountController.id);

                ViewBag.Car = listCar;

                var owner = _ownerModel.FindOwnerById(AccountController.id);
                ViewBag.Revenue = owner.owner_revenue;

                var car = _carModel.FindCarById(carid);
                if (check == 1) 
                {
                    car.car_status = false;
                    _carModel.UpdateCar(car);
                }
                else if(check == 2)
                {
                    car.car_status = true;
                    _carModel.UpdateCar(car);
                }
                else if(check2==1)
                {
                    car.is_update= true;
                    car.update_status = 0;
                    car.update_car_address = update_street_address ?? car.update_car_address;
                    car.update_car_description = update_car_description ?? car.update_car_description;
                    car.update_car_rule= update_car_rule ?? car.update_car_rule;
                    car.update_car_price= update_car_price ?? car.update_car_price;
                    _carModel.UpdateCar(car);
                }    
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
