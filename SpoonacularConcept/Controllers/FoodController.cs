using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;

namespace SpoonacularConcept.Controllers
{
    public class FoodController : BaseController
    {
        public ActionResult Popular()
        {
            CheckCookie();
            if(TempData["userLogInStatus"]!= null)
            {
                return View(TempData["userLogInStatus"]);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}