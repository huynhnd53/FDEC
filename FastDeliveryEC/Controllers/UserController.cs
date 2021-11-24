using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastDeliveryEC.Models
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }    
        public IActionResult UserProfile()
        {
            // If user had already login.
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
    }
}
