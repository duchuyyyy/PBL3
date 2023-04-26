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

        public OwnerController(ApplicationDBContext db)
        {
            _customerModel = new CustomerModel(db);
            _ownerModel = new OwnerModel(db);
            _carModel = new CarModel(db);
            _carRentalModel = new CarRentalModel(db);
            _carRentalCarOwnModel = new CarRentalCarOwnModel(db);
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

            var owner = _ownerModel.FindOwnerById(AccountController.id);
            ViewBag.Revenue = owner.owner_revenue;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult MyCar(int check, int  carid, string description, string district, string ward, string street, string rule, int price)
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
                else if(check == 3)
                {

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
