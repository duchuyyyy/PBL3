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
using MyOToVer1._2.Models.DataModels;

namespace MyOToVer1._2.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly CustomerModel _customerModel;
        private readonly OwnerModel _ownerModel;

        public HomeController(ApplicationDBContext db)
        {
            _customerModel = new CustomerModel(db);
            _ownerModel = new OwnerModel(db);
        }
 
        public IActionResult Index()
        {
            ViewBag.Name = HomeController.username;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public static string EncryptPassword(string pw)
        {
            byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(pw);
            string encryptedPw = Convert.ToBase64String(storePassword);
            return encryptedPw;
        }

        public static string DecryptPassword(string pw)
        {
            byte[] storePassword = Convert.FromBase64String(pw);
            string decryptPw = ASCIIEncoding.ASCII.GetString(storePassword);
            return decryptPw;
        }
        //Get
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer obj)
        {
            if (!ModelState.IsValid)
            {
                bool checkContact = _customerModel.IsValidContact(obj.Contact);
                if (checkContact)
                {
                    ModelState.AddModelError(nameof(Customer.Contact), "Số điện thoại đã tồn tại");
                    return View();
                }
                obj.Password = EncryptPassword(obj.Password);
                _customerModel.AddCustomer(obj);

                var owner = new Owner()
                {
                    Id = obj.Id,
                    owner_revenue = 0,
                    owner_number_account = "Chua co",
                    owner_name_banking  =  "Chua co"
                };
                _ownerModel.AddOwner(owner);
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }

        //Get
        public IActionResult Login()
        {
            return View();
        }

        public static string username;
        public static int id;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string contact, string password)
        {
            if (ModelState.IsValid)
            {
                var data = _customerModel.GetCustomerByContact(contact);
                bool isValidContact = _customerModel.IsValidContact(contact);
                if (isValidContact)
                {
                    password = EncryptPassword(password);
                    bool isValidPassword = _customerModel.IsValidPass(contact, password);
                   
                    if (isValidPassword)
                    {
                        username = data.Name;
                        id = data.Id;

                        //Tao cookie
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.SerialNumber, contact)
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var props = new AuthenticationProperties()
                        {
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                        };
                        
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Customer.Password), "Mật khẩu không đúng");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(Customer.Contact), "Số điện thoại không tồn tại");
                }
            }
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}