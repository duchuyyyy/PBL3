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
        public IActionResult BeCarOwner(Car obj,List<IFormFile> files)
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
                    foreach (var file in files)
                    {
                        if (file != null && file.Length>0)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            var path = Path.Combine("wwwroot\\Images\\Car", filename);
                            using (var stream = new FileStream(path,FileMode.Create))
                            {
                                file.CopyTo(stream);    
                            }
                            var image = new Car_img()
                            {
                                img_name = filename,
                                img_url = path,
                                car_id = obj.car_id
                            };
                            db.Car_Imgs.Add(image);
                        }
                    }
                    db.SaveChanges();   
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("SuccessBeCarOwner", "Customer");
        }

        public IActionResult SuccessBeCarOwner()
        {
            return View();
        }

        
        [Authorize]
        [HttpGet]
        public IActionResult SearchCar(string location, DateTime rentalDateTime, DateTime returnDateTime)
        {
            ViewBag.Name = HomeController.username;
            ViewBag.Location = location;
            ViewBag.ReturnDateTime = returnDateTime;
            ViewBag.RentalDateTime = rentalDateTime;

            if (location == null || rentalDateTime == null || returnDateTime == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (returnDateTime < rentalDateTime)
            {
                return RedirectToAction("Index", "Home");
            }
            
            using(var db = _db)
            {

                var car = db.Cars.Where(p => p.car_number_rented == 0 ? p.car_address.Contains(location) : db.CarRentals
                 .Join(db.Cars, r => r.car_id, c => c.car_id, (r, c) => new { Rental = r, Car = c })
                 .Where(x => x.Car.car_address.Contains(location))
                 .GroupBy(x => x.Rental.car_id)
                 .Select(g => new
                 {
                     car_id = g.Key,
                     rental_datetime = g.Min(o => o.Rental.rental_datetime),
                     return_datetime = g.Max(o => o.Rental.return_datetime)
                 })
                 .Where(x => !(x.rental_datetime < returnDateTime && x.return_datetime > rentalDateTime))
                 .Any(x => x.car_id == p.car_id))
                 .ToList();

                ViewBag.Car = car;
                return View();
            }
        }
        
    }
}
            
