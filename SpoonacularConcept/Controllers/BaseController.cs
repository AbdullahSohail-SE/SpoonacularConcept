using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpoonacularConcept.Models.ViewModels;

namespace SpoonacularConcept.Controllers
{
    public class BaseController : Controller
    {
        public void CheckCookie()
        {
            
            var cookie = HttpContext.Request.Cookies["currentUser"];
            if (cookie != null)
            {
                TempData["userLogInStatus"] = JsonConvert.DeserializeObject<LoginVIewModel>(cookie.Value);
            }

            RedirectToAction("Login", "User");
        }

        
               
    }
}