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
        private readonly ApplicationDBContext db;
        private readonly CarRentalModel _carRentalModel;

        public OwnerController(ApplicationDBContext db)
        {
            _customerModel = new CustomerModel(db);
            _ownerModel = new OwnerModel(db);
            _carModel = new CarModel(db);
            this.db = db;
            _carRentalModel = new CarRentalModel(db);
        }
        [HttpGet]
        public IActionResult OwnerOrder()
        {
            ViewBag.Name = HomeController.username;


            var result = from car in db.Cars
                         join owner in db.Owners on car.owner_id equals HomeController.id
                         join carrental in db.CarRentals on car.car_id equals carrental.car_id
                         where car.car_number_rented != 0 && carrental.deposit_status == 1
                         group carrental by car into g
                         select new CarRentalCarOwn
                         {
                             CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year,
                             RentalDateTime = g.Select(x => x.rental_datetime).FirstOrDefault(),
                             ReturnDateTime = g.Select(x => x.return_datetime).FirstOrDefault(),
                             CustomerName = g.Select(x => x.customer.Name).FirstOrDefault(),
                             CustomerContact = g.Select(x => x.customer.Contact).FirstOrDefault(),
                             Price = g.Select(x => x.total_price).FirstOrDefault()
                         };

            var result2 = (from car in db.Cars
                          join owner in db.Owners on car.owner_id equals HomeController.id
                          join carrental in db.CarRentals on car.car_id equals carrental.car_id
                          where car.car_number_rented != 0 && carrental.deposit_status == 1
                          select new CarRentalCarOwn
                          {
                              rentalId = carrental.rental_id,
                              CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                              RentalDateTime = carrental.rental_datetime,
                              ReturnDateTime = carrental.return_datetime,
                              CustomerName = carrental.customer.Name,
                              CustomerContact = carrental.customer.Contact,
                              Price = carrental.total_price
                          }).Distinct();

            var result3 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.deposit_status == 2 && carrental.rental_status == 2
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            var result4 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.rental_status == 3
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            var result5 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.rental_status == 4
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            ViewBag.Car = result2.ToList();

            ViewBag.Car2 = result3.ToList();

            ViewBag.Car3 = result4.ToList();

            ViewBag.Car4 = result5.ToList();
           
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
            var result2 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.deposit_status == 1
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            var result3 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.deposit_status == 2 && carrental.rental_status == 2
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            var result4 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.rental_status == 3
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            var result5 = (from car in db.Cars
                           join owner in db.Owners on car.owner_id equals HomeController.id
                           join carrental in db.CarRentals on car.car_id equals carrental.car_id
                           where car.car_number_rented != 0 && carrental.rental_status == 4
                           select new CarRentalCarOwn
                           {
                               rentalId = carrental.rental_id,
                               CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                               RentalDateTime = carrental.rental_datetime,
                               ReturnDateTime = carrental.return_datetime,
                               CustomerName = carrental.customer.Name,
                               CustomerContact = carrental.customer.Contact,
                               Price = carrental.total_price
                           }).Distinct();

            ViewBag.Car = result2.ToList();
            ViewBag.Car2 = result3.ToList();
            ViewBag.Car3 = result4.ToList();
            ViewBag.Car4 = result5.ToList();
            return View();
        }
    }
}
