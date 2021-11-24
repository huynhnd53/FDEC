using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FastDeliveryEC.Models;
using System.Collections.Generic;
using static DataLibrary.BusinessRule.UserProcessor;

namespace FastDeliveryEC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //If user had already login.
            if (HttpContext.Session.GetString("username") != null)
            {
                //Redirect to Edit Profile
                return RedirectToAction("UserProfile", "User");
            }
            return View("Login");
        }

        public IActionResult Login(LoginModel model)
        {
            // If user had already login.
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("ViewHome", "Home");
            }
            // If ModelState is invalid, return to Login.cshtml.
            if (!ModelState.IsValid)
            {
                return View();
            }
            // If login information is valid, login.
            if (IsValidLogin(model.Username, model.Password))
            {
                //// Add username to Session.
                HttpContext.Session.SetString("username", model.Username);
                //// Add profile to Session.
                DataLibrary.Models.UserModel thisUser = GetAccountIdByUsername(model.Username);
                //thisProfile = getProfilebyUsername(model.Username);  
                HttpContext.Session.SetObjectAsJson("userss", thisUser);
                //HttpContext.Session.SetString("noOfPosts", thisProfile.NoOfPosts.ToString());
                //HttpContext.Session.SetString("follower", thisProfile.Follower.ToString());
                //HttpContext.Session.SetString("following", thisProfile.Following.ToString());
                //HttpContext.Session.SetString("profileid", thisProfile.ProfileID.ToString());
                //ViewBag.sessionu = model.Username;
                //// TODO: redirect to home page.
                //List<ProfileModel> topFollowProfile = GetTopFollowerProfile(3, thisProfile.ProfileID);
                //HttpContext.Session.SetObjectAsJson("topFollowProfile", topFollowProfile);
                return RedirectToAction("ViewHome", "Home");
            }
            else
            {
                ViewData["LoginError"] = "Wrong username or password";
                return View();
            }
        }

        public IActionResult Register()
        {
            // If user had already login.
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("UserProfile", "User");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            // If user had already login.
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("UserProfile", "User");
            }
            // If ModelState is invalid, return to Register.cshtml.
            if (!ModelState.IsValid)
                return View();
            // TODO: Add new account to database and redirect to other page.
            if (IsExistedUsername(model.Username))
            {
                ViewData["RegisterError"] = "Duplicated Username";
                return View();
            }
            // If Account should have been created successfully.
            if (CreateAccount(model.Username, model.Password) == 1)
            {
                // Add username to Session.
                HttpContext.Session.SetString("username", model.Username);
                // Create new corresponding profile.
                DataLibrary.Models.UserModel thisUser = GetAccountIdByUsername(model.Username);
                HttpContext.Session.SetObjectAsJson("userss", thisUser);
                // TODO: Redirect to profile page.
                return RedirectToAction("UserProfile", "User");
            }
            else
            {
                ViewData["RegisterError"] = "Something went wrong, please try again";
                return View();
            }
        }

        public IActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPass(ForgotPassModel model)
        {
            ViewData["Username"] = model.Username;
            ForgotPassModel f = new ForgotPassModel
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                RePassword = model.RePassword
            };
            ViewData["ForgotPassModel"] = f;

            if (!IsEmailOfUsername(model.Username, model.Email))
            {
                ViewData["ForgotPassError"] = "Email not match with Username";
                return View("ForgotPass");
            }
            return View("ResetPass");
        }

        //public IActionResult ResetPass()
        //{
        //    return View("ResetPass");
        //    //ViewData["ResetPassError"] = "Something went wrong, please try again";
        //}

        [HttpPost]
        public IActionResult ResetPass(ForgotPassModel model)
        {
            ViewData["Username"] = model.Username;
            //ViewData["Username"] = model.Username;
            ForgotPassModel f = new ForgotPassModel
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                RePassword = model.RePassword
            };
            ViewData["ForgotPassModel"] = f;
            if (!model.Password.Equals(model.RePassword))
            {
                ViewData["LoginError"] = "Password and RePassword not match";
                return View("ResetPass");
            }
            else
            {
                if (UpdatePassword(model.Username, model.Password) == 1)
                {
                     ViewData["PasswordSuccess"] = "Reset password success !!!";
                }
                else
                {
                    ViewData["PasswordError"] = "Reset password fail";
                }
            }
            return View("Login");

            //ViewData["ResetPassError"] = "Something went wrong, please try again";
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("userss");
            return RedirectToAction("Index");
        }
    }
}
