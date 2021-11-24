using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataLibrary.BusinessRule.UserProcessor;
using static DataLibrary.BusinessRule.RuleOfDistanceProcess;
using static DataLibrary.BusinessRule.RulePriceProcess;
using static DataLibrary.BusinessRule.OrdersProcess;
using static DataLibrary.BusinessRule.CityProcess;



namespace FastDeliveryEC.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult CreateOrder()
        {
            string username = HttpContext.Session.GetString("username");
            UserModel user = GetAccountIdByUsername(username);
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            List<CityModel> city = GetListCity();
            ViewData["ListCity"] = city;
            List<RulePriceModel> rulePrices = GetListRulePrices();
            ViewData["ListRulePrice"] = rulePrices;

            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(FastDeliveryEC.Models.OrderModel model)
        {
            string username = HttpContext.Session.GetString("username");
            UserModel user = GetAccountIdByUsername(username);
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }

            DataLibrary.Models.UserModel thisUser = GetAccountIdByUsername(user.Username);
            HttpContext.Session.SetObjectAsJson("userss", thisUser);

            List<CityModel> city = GetListCity();
            ViewData["ListCity"] = city;
            List<RulePriceModel> rulePrices = GetListRulePrices();
            ViewData["ListRulePrice"] = rulePrices;

            RuleOfDistanceModel ruleDistance = GetRuleOfDistance(model.AddressFrom, model.AddressOrder);
            RulePriceModel rulePrice = GetRulePricesByID(model.WeightID);
            if (ruleDistance != null)
            {
                float cashTempt = (float)ruleDistance.Km * 1000 + (float)rulePrice.Price;

                if (model.NumberPhoneOrder == "" || model.NumberPhoneOrder == null)
                {
                    ViewData["NumberPhoneOrder"] = "Number Phone Order, please try again";
                    return View();
                }


                OrdersModel ord = new OrdersModel
                {
                    CreaterID = user.ID,
                    ShipperID = 0,
                    CreateDate = DateTime.Now,
                    ExpiredDate = DateTime.Now,
                    SuccessDate = DateTime.Now,
                    AddressFrom = model.AddressFrom,
                    AddressOrder = model.AddressOrder,
                    StatusOrder = 0,
                    NumberPhoneOrder = model.NumberPhoneOrder,
                    Cash = cashTempt,
                    WeightID = model.WeightID,
                    IsActive = true

                };
                if (CreateOrders(ord) == 1)
                {

                    return RedirectToAction("CreateSuccessOrder", "Order");
                }
                else
                {
                    ViewData["CreateFailOrder"] = "Something went wrong, please try again";
                    return View();
                }

            }
            else
            {
                ViewData["CreateFailOrder"] = "Something went wrong, please try again";
                return View();
            }
        }

        public IActionResult CreateSuccessOrder()
        {
            return View();
        }

        public IActionResult SearchOrderStatus()
        {
            string username = HttpContext.Session.GetString("username");
            UserModel users = GetAccountIdByUsername(username);
            if (users == null)
            {
                return RedirectToAction("Login", "Login");
            }
            DataLibrary.Models.UserModel thisUser = GetAccountIdByUsername(users.Username);
            HttpContext.Session.SetObjectAsJson("userss", thisUser);
            return View();
        }

        public IActionResult OrderQuotes()
        {
            return View();
        }

        public IActionResult ListOrder()
        {
            string username = HttpContext.Session.GetString("username");
            UserModel user = GetAccountIdByUsername(username);
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            DataLibrary.Models.UserModel thisUser = GetAccountIdByUsername(user.Username);
            HttpContext.Session.SetObjectAsJson("userss", thisUser);

            List<OrdersModel> lstOrder = GetListOrderByUserID(user.ID);
            List<FastDeliveryEC.Models.ViewOrderModel> lstViewOrderModel = new List<FastDeliveryEC.Models.ViewOrderModel>();
            foreach (var x in lstOrder)
            {
                FastDeliveryEC.Models.ViewOrderModel v = new FastDeliveryEC.Models.ViewOrderModel();
                v.AddressFrom = GetCityByID(x.AddressFrom).Name.ToString();
                v.AddressOrder = GetCityByID(x.AddressOrder).Name.ToString();
                v.Status = IsStatus(x.StatusOrder);
                v.Cash = x.Cash;
                lstViewOrderModel.Add(v);
            }

            if (lstOrder != null)
            {
                ViewData["MyListOrder"] = lstViewOrderModel;
                return View();
            }
            else
            {
                ViewData["MyListOrderEmpty"] = "DONT HAVE ANY ORDER FOR YOU";
                return View();
            }

        }

        public string IsStatus(int id)
        {
            string result = "";
            if (id == 0)
            {
                result = "Chờ lấy hàng";
            }
            else if (id == 1)
            {
                result = "Đang vận giao hàng";
            }
            else if (id == 2)
            {
                result = "Giao hàng thành công";
            }
            return result;
        }

        //public IActionResult ListOrder(OrdersModel model)
        //{
        //    string username = HttpContext.Session.GetString("username");
        //    UserModel user = GetAccountIdByUsername(username);
        //    if (user == null)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //    List<OrdersModel> lstOrder = GetListOrderByUserID(user.ID);
        //    ViewData["MyListOrder"] = lstOrder;
        //    return View();
        //}

        public async Task<ActionResult> AjaxCreateOrder()
        {
            string username = HttpContext.Session.GetString("username");
            var user = GetAccountIdByUsername(username);

            var ouput = new JsonResult(new
            {
                responseCode = 0,
                message = "Fail",
                data = user
            });

            return ouput;
        }
        public async Task<ActionResult> AjaxGetListCity()
        {
            List<CityModel> city = GetListCity();
            var ouput = new JsonResult(new
            {
                responseCode = 1,
                message = "Success",
                data = city
            });
            return ouput;
        }
        public async Task<ActionResult> AjaxGetListWeight()
        {
            List<RulePriceModel> weight = GetListRulePrices();
            var ouput = new JsonResult(new
            {
                responseCode = 1,
                message = "Success",
                data = weight
            });
            return ouput;
        }

        public async Task<ActionResult> AjaxCalculateCash(int addressFrom, int addressOrder, int weightID)
        {
            RuleOfDistanceModel ruleDistance = GetRuleOfDistance(addressFrom, addressOrder);
            RulePriceModel rulePrice = GetRulePricesByID(weightID);
            float cashTempt = 0;
            if (ruleDistance != null)
            {
                cashTempt = (float)ruleDistance.Km * 1000 + (float)rulePrice.Price;
            }
            var ouput = new JsonResult(new
            {
                responseCode = 1,
                message = "Success",
                data = cashTempt
            });
            return ouput;
        }

    }
}
