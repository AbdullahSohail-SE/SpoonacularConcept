using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpoonacularConcept.Models;
using SpoonacularConcept.Models.ViewModels;
using Newtonsoft.Json;

namespace SpoonacularConcept.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Login()
        {
            if(TempData["userLogInStatus"] != null)
            {
                return View(TempData["userLogInStatus"]);
            }
            return View(new LoginVIewModel());
        }
        public ActionResult SignOut()
        {
            HttpContext.Response.Cookies["currentUser"].Expires = new DateTime(1, 1, 1);
                TempData["userLogInStatus"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult RegisterUser(User user)
        {
            var manager = new DBManager("SpoonacularDB");
            var userId=manager.RegisterUser(user);
            TempData["userLogInStatus"] = new LoginVIewModel { Email = user.Email, Name = user.Name, authStatus = AuthStatus.Authenticated,UserId=userId};
            var cookie = new HttpCookie("currentUser", JsonConvert.SerializeObject(TempData["userLogInStatus"]));
            cookie.Expires = DateTime.Now.AddMinutes(30);
            HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Popular", "Food");
        }
        public ActionResult AuthenticateUser(User user)
        {
            var manager = new DBManager("SpoonacularDB");
            var authResult = manager.authenticateUser(user);
            var status = (AuthStatus)authResult.Status;
            if (status == AuthStatus.UserNotFound)
            {
                TempData["userLogInStatus"] = new LoginVIewModel { authStatus = status };
                return RedirectToAction("Login");
            }
            if (status == AuthStatus.WrongPassword)
            {
                TempData["userLogInStatus"] = new LoginVIewModel { Email = user.Email, Name = user.Name, authStatus = status };
                return RedirectToAction("Login");
            }
            if (status == AuthStatus.Authenticated)
            {
                TempData["userLogInStatus"] = new LoginVIewModel { Email = user.Email, Name = user.Name, authStatus = status,UserId=authResult.UserId };
                var cookie = new HttpCookie("currentUser",JsonConvert.SerializeObject(TempData["userLogInStatus"]));
                cookie.Expires= DateTime.Now.AddMinutes(30);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Popular", "Food");

            }
            return Content("Error :(");
            
        }
    }
}