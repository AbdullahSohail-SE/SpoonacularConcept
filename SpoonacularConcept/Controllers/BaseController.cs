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
        public bool CookieExists()
        {
            
            
            var cookie = HttpContext.Request.Cookies["currentUser"];
            if (cookie != null )
            {
                if (String.IsNullOrEmpty(cookie.Value))
                    return false;
                
                    TempData["userLogInStatus"] = JsonConvert.DeserializeObject<LoginVIewModel>(cookie.Value);
                     return true;
            }
            else
                return false;
           
        }

        



    }
}