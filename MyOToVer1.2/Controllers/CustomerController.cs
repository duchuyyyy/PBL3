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
        
    }
}
