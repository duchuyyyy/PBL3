using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models;
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
        public IActionResult Login(string contact, string password)
        {
            //bool isValid = contact.Equals("0337164315") && password.Equals("123");
            //if(isValid)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //return Content("Error");

            ApplicationDBContext dBContext = _db;
            var data = dBContext.Customers.Where(e => e.Contact.Equals(contact)).SingleOrDefault();
            bool isValid = dBContext.Customers.Any(dbcontext => dbcontext.Contact.Equals(contact) &&  DecryptPassword(data.Password) == password);
            if(isValid)
            {
                return RedirectToAction("Register", "Customer");
            }
            else
            {
                //Lay chuoi string truyen vao so sanh, so sanh vay la sai roi
                bool isValid1 = dBContext.Customers.Any(dbcontext => dbcontext.Contact.Equals("0337164315") && dbcontext.Password.Equals("123"));
                if (isValid1)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return Content("Error");
        }
    }
}
