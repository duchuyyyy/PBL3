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
        public IActionResult Index()
        {
            ViewBag.Name = AccountController.username;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}