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
namespace MyOToVer1._2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
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
            if (ModelState.IsValid)
            {
                bool checkContact = _db.Customers.Any(c => c.Contact.Equals(obj.Contact));
                if (checkContact)
                {
                    ModelState.AddModelError(nameof(Customer.Contact), "Số điện thoại đã tồn tại");
                    return View();
                }
                obj.Password = EncryptPassword(obj.Password);
                _db.Customers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }

        //Get
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string contact, string password)
        {
            if (ModelState.IsValid)
            {
                ApplicationDBContext dBContext = _db;
                var data = dBContext.Customers.Where(e => e.Contact.Equals(contact)).SingleOrDefault();

                bool isValidContact = dBContext.Customers.Any(dbcontext => dbcontext.Contact.Equals(contact));
                if (isValidContact)
                {
                    bool isValidPassword = dBContext.Customers.Any(dbcontext => dbcontext.Contact.Equals(contact) && DecryptPassword(data.Password) == password);
                    if (isValidPassword)
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.SerialNumber, contact)
                };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var props = new AuthenticationProperties();
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                        return RedirectToAction("MainPage", "Customer");
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