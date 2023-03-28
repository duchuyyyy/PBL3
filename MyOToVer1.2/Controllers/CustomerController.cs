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

namespace MyOToVer1._2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CustomerController(ApplicationDBContext db)
        {
            _db = db;
        }
        
        [Authorize]
        public IActionResult BeCarOwner()
        {
            ViewBag.Name = HomeController.username;
            return View();
        }

        [HttpPost]
        public IActionResult BeCarOwner(Car obj)
        {    
            using(var db = _db)
            {
                var owner = db.Owners.Find(HomeController.id);
                

                bool checkCarNumber = db.Cars.Any(p => p.car_number.Equals(obj.car_number));
                if (!checkCarNumber)
                {
                    obj.owner_id = owner.Id;
                    obj.car_status = false;
                    obj.car_number_rented = 0;
                    obj.car_address = obj.car_street_address + ", " + obj.car_ward_address + ", " + obj.car_address;
                    db.Cars.Add(obj);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        
        [Authorize]
        [HttpGet]
        public IActionResult SearchCar(string location, DateTime rentalDateTime, DateTime returnDateTime)
        {
            ViewBag.Name = HomeController.username;
            ViewBag.Location = location;
            ViewBag.ReturnDateTime = returnDateTime;
            ViewBag.RentalDateTime = rentalDateTime;


            if (returnDateTime < rentalDateTime)
            {
                return RedirectToAction("Index", "Home");
            }
            
            using(var db = _db)
            {
                //bool check = db.Cars.Any(p => p.car_address.Contains(location) && p.car_number_rented == 0);
                //if(check)
                //{
                //    return Content("Tim duoc");
                //}
                //else
                //{
                //    return Content("Khong tim duoc");
                //}
                
                return View();
            }
        }
        
    }
}
