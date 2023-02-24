using Microsoft.AspNetCore.Mvc;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models;

namespace MyOToVer1._2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CustomerController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(Customer obj)
        {
            _db.Customers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home"); 
        }
        
    }
}
