using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FastDeliveryEC.Models;
using System.Collections.Generic;
using static DataLibrary.BusinessRule.UserProcessor;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FastDeliveryEC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("index");
        }
        /// <summary>
        /// View Home Page
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewHome()
        {
            string username = HttpContext.Session.GetString("username");
            var users = GetAccountIdByUsername(username);
            if (users != null)
            {
                DataLibrary.Models.UserModel thisUser = GetAccountIdByUsername(users.Username);
                HttpContext.Session.SetObjectAsJson("userss", thisUser);
            }
            return View();
        }

        public async Task<ActionResult> AjaxGetDetailRecordByUser()
        {
            string username = HttpContext.Session.GetString("username");
            var user =  GetAccountIdByUsername(username);
            return new JsonResult(new {
                responseCode = 1,
                message = "success", 
                data = user
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
