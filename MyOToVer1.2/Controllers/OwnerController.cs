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
        public IActionResult OwnerOrder()
        {
            ViewBag.Name = HomeController.username;

            var listNotConfirm = _carRentalCarOwnModel.GetListNotConfirm(HomeController.id);

            var listConfirmed = _carRentalCarOwnModel.GetListConfirmed(HomeController.id);

            var listWaitToHandOverCar = _carRentalCarOwnModel.GetListWaiToHandOverCar(HomeController.id);

            var listOrderCompleted = _carRentalCarOwnModel.GetListOrderCompleted(HomeController.id);

            ViewBag.Car = listNotConfirm;

            ViewBag.Car2 = listConfirmed;

            ViewBag.Car3 = listWaitToHandOverCar;

            ViewBag.Car4 = listOrderCompleted;
           
            return View();
        }

        [HttpPost]
        public IActionResult OwnerOrder(int count, int id, DateTime rentalDateTime)
        {

            ViewBag.Name = HomeController.username;
            try
            {
                if (count == 1)
                {
                    var carRental = _carRentalModel.FindCarRentalById(id);
                    carRental.deposit_status = 2;
                    carRental.rental_status = 2;
                    _carRentalModel.UpdateCarRental(carRental);
                }
                else if(count == 2)
                {
                    var carRental = _carRentalModel.FindCarRentalById(id);
                    DateTime rentalDate = rentalDateTime.Date;
                    if(rentalDate > DateTime.Today)
                    {
                        return Content("Khong duoc phep giao xe truoc ngay thue!");
                    }
                    else
                    {
                        carRental.rental_status = 3;
                        _carRentalModel.UpdateCarRental(carRental);
                    }
                }
                else if(count == 3)
                {
                    var carRental = _carRentalModel.FindCarRentalById(id);
                    carRental.rental_status = 4;
                    var owner = _ownerModel.FindOwnerById(HomeController.id);
                    if(owner != null)
                    {
                        owner.owner_revenue += carRental.total_price;
                    }
                    _ownerModel.UpdateOwner(owner);
                    _carRentalModel.UpdateCarRental(carRental);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var listNotConfirm = _carRentalCarOwnModel.GetListNotConfirm(HomeController.id);

            var listConfirmed = _carRentalCarOwnModel.GetListConfirmed(HomeController.id);

            var listWaitToHandOverCar = _carRentalCarOwnModel.GetListWaiToHandOverCar(HomeController.id);

            var listOrderCompleted = _carRentalCarOwnModel.GetListOrderCompleted(HomeController.id);

            ViewBag.Car = listNotConfirm;

            ViewBag.Car2 = listConfirmed;

            ViewBag.Car3 = listWaitToHandOverCar;

            ViewBag.Car4 = listOrderCompleted;
            return View();
        }

        [HttpGet]
        public IActionResult MyCar()
        {
            ViewBag.Name = HomeController.username;

            return View();
        }
    }
}
