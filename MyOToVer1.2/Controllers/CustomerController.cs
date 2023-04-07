using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using MyOToVer1._2.Models.DataModels;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Controllers
{
    public class CustomerController : Controller
    {
        
        private readonly CarModel _carModel;
        private readonly OwnerModel _ownerModel;
        public CustomerController(ApplicationDBContext db)
        {
            _carModel = new CarModel(db);
            _ownerModel = new OwnerModel(db);
        }

        [HttpGet]        
        [Authorize]
        public IActionResult BeCarOwner()
        {
            ViewBag.Name = HomeController.username;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult BeCarOwner(CarOwnerViewModels obj)
        {

            var owner = _ownerModel.FindOwnerById(HomeController.id);

            owner.owner_number_account = obj.Owner.owner_number_account;
            owner.owner_name_banking = obj.Owner.owner_name_banking;

            _ownerModel.UpdateOwner(owner);
            bool checkCarNumber = _carModel.IsValidCarNumber(obj.Car.car_number);
            if (!checkCarNumber)
            {
                obj.Car.owner_id = owner.Id;
                obj.Car.car_status = false;
                obj.Car.car_number_rented = 0;
                obj.Car.car_address = obj.Car.car_street_address + ", " + obj.Car.car_ward_address + ", " + obj.Car.car_address;

                //foreach (var file in files)
                //{
                //    if (file != null && file.Length > 0)
                //    {
                //        var filename = Path.GetFileName(file.FileName);
                //        var path = Path.Combine("wwwroot\\Images\\Car", filename);
                //        using (var stream = new FileStream(path, FileMode.Create))
                //        {
                //            file.CopyTo(stream);
                //        }

                //        obj.Car.car_name_img = filename;

                //    }
                //}
                _carModel.AddCar(obj.Car);
                return RedirectToAction("SuccessBeCarOwner", "Customer");
            }
            else
            {
                return View();
            }
        }

        public IActionResult SuccessBeCarOwner()
        {
            ViewBag.Name = HomeController.username;
            return View();
        }

        
        [Authorize]
        [HttpGet]
        public IActionResult SearchCar(string location, DateTime rentalDateTime, DateTime returnDateTime)
        {
            ViewBag.Name = HomeController.username;
            ViewBag.Customer_Id = HomeController.id;
            ViewBag.Location = location;
            ViewBag.ReturnDateTime = returnDateTime;
            ViewBag.RentalDateTime = rentalDateTime;
            TimeSpan difference = returnDateTime.Subtract(rentalDateTime);
            double totalDays = difference.TotalDays;
            ViewBag.NumberDateRented = totalDays;

            
            
                if (returnDateTime < rentalDateTime)
                {
                    return RedirectToAction("Index", "Home");
                }

               
                bool check = _carModel.IsValidCusOwn(HomeController.id);

                    if (!check)
                    {
                        var car = _carModel.SearchCar(location, rentalDateTime, returnDateTime);
                        ViewBag.Car = car;
                    }
                    else
                    {
                        return Content("Khong tim duoc xe phu hop");
                    }
                return View();
                
        }

        [HttpGet]
        [Authorize]
        public IActionResult ConfirmBooking(int car_id, int customer_id, double totalPrice, DateTime rentalDateTime, DateTime returnDateTime)
        {
            ViewBag.Name = HomeController.username;
            return View();
        }


    }
}
            
