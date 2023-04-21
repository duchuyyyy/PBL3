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
using System;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MyOToVer1._2.Controllers
{
    public class CustomerController : Controller
    {
        
        private readonly CarModel _carModel;
        private readonly OwnerModel _ownerModel;
        private readonly CustomerModel _customerModel;
        private readonly CarImgModel _carImgModel;
        private readonly CarRentalModel _carRentalModel;
        private readonly TransferEvidencePhotoModel _transferEvidenceModel;
        private readonly CarRentalCarCusModel _carRentalCarCusModel;
        private readonly CarReviewModel _carReviewModel;
        private readonly CarReviewCustomerModel _carReviewCustomerModel;

        public CustomerController(ApplicationDBContext db)
        {
            _carModel = new CarModel(db);
            _ownerModel = new OwnerModel(db);
            _customerModel = new CustomerModel(db);
            _carImgModel = new CarImgModel(db);
            _carRentalModel = new CarRentalModel(db);
            _transferEvidenceModel = new TransferEvidencePhotoModel(db);
            _carRentalCarCusModel = new CarRentalCarCusModel(db);
            _carReviewModel = new CarReviewModel(db);
            _carReviewCustomerModel = new CarReviewCustomerModel(db);
        }

        [HttpGet]        
        [Authorize(Roles = "User")]
        public IActionResult BeCarOwner()
        {

            try
            {
                ViewBag.Name = AccountController.username;
                return View();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult BeCarOwner(CarOwnerViewModels obj, List<IFormFile> files)
        {
            var owner = _ownerModel.FindOwnerById(AccountController.id);
            owner.owner_number_account = obj.Owner.owner_number_account;
            owner.owner_name_banking = obj.Owner.owner_name_banking;
            
            _ownerModel.UpdateOwner(owner);
            bool checkCarNumber = _carModel.IsValidCarNumber(obj.Car.car_number);
            if (!checkCarNumber)
            {
                obj.Car.owner_id = owner.Id;
                obj.Car.car_status = true;
                obj.Car.car_number_rented = 0;
                obj.Car.car_address = obj.Car.car_street_address + ", " + obj.Car.car_ward_address + ", " + obj.Car.car_address;
                _carModel.AddCar(obj.Car);
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine("wwwroot\\Images\\Car", filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var Img = new CarImg
                        {
                            name_img = filename,
                            car_id = obj.Car.car_id,
                        };
                        _carImgModel.AddImg(Img);
                    }
                }

                return RedirectToAction("SuccessBeCarOwner", "Customer");
            }
            else
            {
                return View();
            }
        }

        public IActionResult SuccessBeCarOwner()
        {
            ViewBag.Name = AccountController.username;
            return View();
        }

        
        [Authorize]
        [HttpGet]
        public IActionResult SearchCar(string location, DateTime rentalDateTime, DateTime returnDateTime)
        {
            
            ViewBag.Name = AccountController.username;
            ViewBag.Customer_Id = AccountController.id;
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
                
            var car = _carModel.SearchCar(location, rentalDateTime, returnDateTime, AccountController.id);
            ViewBag.Car = car;

            foreach (var item in car)
            {
                var customer = _customerModel.FindCustomerById(item.owner_id);
                ViewBag.OwnerName = customer.Name;
            }

            var img = _carImgModel.FindImageByCar(car);
            ViewBag.Img = img;

            var review = _carReviewCustomerModel.GetReviewByCar(car);
            ViewBag.Review = review;
            var reviewScore = _carReviewCustomerModel.GetReviewScore(car);
            ViewBag.ReviewSocre = reviewScore;
            return View();
        }
        
        [HttpPost]
        public IActionResult SearchCar(int sortValue, int price, string brand, string location, int capacity, DateTime rentalDateTime, DateTime returnDateTime)
        {
            ViewBag.Name = AccountController.username;
            ViewBag.Customer_Id = AccountController.id;
            ViewBag.Location = location;
            ViewBag.ReturnDateTime = returnDateTime;
            ViewBag.RentalDateTime = rentalDateTime;
            TimeSpan difference = returnDateTime.Subtract(rentalDateTime);
            double totalDays = difference.TotalDays;
            ViewBag.NumberDateRented = totalDays;

            var car = _carModel.SearchCar(location, rentalDateTime, returnDateTime, AccountController.id);

            if (sortValue == 2)
            {
                car = _carModel.OrderByAscPrice(location, price, car);
            }
            else if (sortValue == 3)
            {
                car = _carModel.OrderByDescPrice(location, price, car);
            }

            car = _carModel.FilterByPrice(location, price, car);

            if (brand != "All")
            {
                car = _carModel.FilterByBrand(location, brand, car);
            }

            car = _carModel.FilterByCapacity(location, capacity, car);

            ViewBag.Car = car;

           
            var img = _carImgModel.FindImageByCar(car);
            ViewBag.Img = img;

            var review = _carReviewCustomerModel.GetReviewByCar(car);
            ViewBag.Review = review;

            return View();
        }

        public static int carid;
        public static int customerid;
        public static double totalprice;
        public static DateTime rentaldatetime;
        public static DateTime returndatetime;
        
        [HttpGet]
        [Authorize]
        public IActionResult ConfirmBooking(int car_id, int customer_id, double totalPrice, DateTime rentalDateTime, DateTime returnDateTime)
        {
            ViewBag.Name = AccountController.username;

            try
            {
                var car = _carModel.FindCarById(car_id);
                

                var ownerInfo = _customerModel.FindCustomerById(car.owner_id);
                var ownerDetailInfo = _ownerModel.FindOwnerById(car.owner_id);
                ViewBag.NameOwner = ownerInfo.Name;
                ViewBag.ContactOwner = ownerInfo.Contact;
                ViewBag.NumberAccount = ownerDetailInfo.owner_number_account;
                ViewBag.NameBanking = ownerDetailInfo.owner_name_banking;


                carid = car_id;
                customerid = customer_id;
                totalprice = totalPrice;
                rentaldatetime = rentalDateTime;
                returndatetime = returnDateTime;

                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        [HttpPost]
        public IActionResult ConfirmBooking(IFormFile file)
        {
            try
            {

                int car_id = carid;
                int customer_id = customerid;
                double totalPrice = totalprice;
                DateTime rentalDateTime = rentaldatetime;
                DateTime returnDateTime = returndatetime;


                var carRental = new CarRental()
                {
                    rental_datetime = rentalDateTime,
                    return_datetime = returnDateTime,
                    car_id = car_id,
                    customer_id = customer_id,
                    total_price = totalPrice,
                    rental_status = 1,
                    deposit_status = 1
                };

                bool isDuplicateCarRental = _carRentalModel.isDuplicateCarRental(carRental.rental_datetime, carRental.return_datetime, carRental.customer_id, carRental.car_id, carRental.total_price);

                if(isDuplicateCarRental)
                {
                    return Content("Da bi loi");
                }
                else
                {
                    _carRentalModel.AddCarRental(carRental);

                    var car = _carModel.FindCarById(car_id);
                    car.car_number_rented += 1;

                    _carModel.UpdateCar(car);



                    if (file != null && file.Length > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine("wwwroot\\Images\\EvidencePhotos", filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var imgEvidence = new TransferEvidencePhoto()
                        {
                            name_img = filename,
                            rental_id = carRental.rental_id
                        };
                        _transferEvidenceModel.AddEvidencePhoto(imgEvidence);
                    }
                    return RedirectToAction("SuccessPayment");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        public IActionResult MyBooking()
        {
            try
            {
                ViewBag.Name = AccountController.username;

                var myListBooking = _carRentalCarCusModel.GetListNotConfirmed(AccountController.id);

                var myListBooking2 = _carRentalCarCusModel.GetListConfirmed(AccountController.id);

                var myListBooking3 = _carRentalCarCusModel.GetListOrderIsCompleting(AccountController.id);

                var myListBooking4 = _carRentalCarCusModel.GetListOrderCompleted(AccountController.id);

                ViewBag.ListCarRental = myListBooking;


                ViewBag.ListCarRental2 = myListBooking2;

                ViewBag.ListCarRental3 = myListBooking3;

                ViewBag.ListCarRental4 = myListBooking4;

                return View();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult SuccessPayment()
        {
            try
            {
                int car_id = carid;
                var car = _carModel.FindCarById(car_id);
                ViewBag.Car = car.car_name + " " + car.car_brand;
                ViewBag.Address = car.car_address;
                ViewBag.Time = rentaldatetime;
                ViewBag.Name = AccountController.username;
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult ReviewContent(int customerid, int carid)
        {
            try
            {
                ViewBag.Name = AccountController.username;
                ViewBag.CarId = carid;
                ViewBag.CustomerId = customerid;
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult ReviewContent(CarReview obj)
        {
            ViewBag.Name = AccountController.username;
            try
            {
                _carReviewModel.AddCarReview(obj);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
            
