using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models;
using System.Linq;
using System.Text;

namespace MyOToVer1._2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CustomerController(ApplicationDBContext db)
        {
            _db = db;
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
            bool checkContact = _db.Customers.Any(c => c.Contact.Equals(obj.Contact));
            if(checkContact)
            {
                return Content("Contact is already exist");
            }
            obj.Password = EncryptPassword(obj.Password);
            _db.Customers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home"); 
        }

        //Get
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(string contact, string password)
        {
            ApplicationDBContext dBContext = _db;
            var data = dBContext.Customers.Where(e => e.Contact.Equals(contact)).SingleOrDefault();
            bool isValidContact = dBContext.Customers.Any(dbcontext => dbcontext.Contact.Equals(contact));
            if(isValidContact)
            {
                bool isValidPassword = dBContext.Customers.Any(dbcontext => dbcontext.Contact.Equals(contact) && DecryptPassword(data.Password) == password);
                if (isValidPassword)
                {
                    HttpContext.Session.SetString(contact, data.Contact);
                    return RedirectToAction("MainPage", "Customer");
                }
            }
            return Content("Error");
        }

        public IActionResult MainPage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
