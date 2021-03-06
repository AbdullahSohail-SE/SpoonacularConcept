﻿using System;
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
            if(Session["userLogInStatus"] != null)
            {
                return View(Session["userLogInStatus"]);
            }
            return View(new LoginVIewModel());
        }
        public ActionResult SignOut()
        {
            HttpContext.Response.Cookies["currentUser"].Expires = new DateTime(1, 1, 1);
                Session["userLogInStatus"] = null;
            Session.Abandon();
            return RedirectToAction("Login");
        }
        [ChildActionOnly]
        public ActionResult Navbar()
        {
            var userStatusModel = Session["userLogInStatus"];
            return PartialView("_Navbar", userStatusModel);
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult RegisterUser(User user)
        {
            var manager = new DBManager("SpoonacularDB");
            var userId=manager.RegisterUser(user);
            Session["userLogInStatus"] = new LoginVIewModel { Email = user.Email, Name = user.Name, authStatus = AuthStatus.Authenticated,UserId=userId};
            var cookie = new HttpCookie("currentUser", JsonConvert.SerializeObject(Session["userLogInStatus"]));
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
                Session["userLogInStatus"] = new LoginVIewModel { authStatus = status };
                return RedirectToAction("Login");
            }
            if (status == AuthStatus.WrongPassword)
            {
                Session["userLogInStatus"] = new LoginVIewModel { Email = user.Email, Name = user.Name, authStatus = status };
                return RedirectToAction("Login");
            }
            if (status == AuthStatus.Authenticated)
            {
                Session["userLogInStatus"] = new LoginVIewModel { Email = user.Email, Name = user.Name, authStatus = status,UserId=authResult.UserId };
                var cookie = new HttpCookie("currentUser",JsonConvert.SerializeObject(Session["userLogInStatus"]));
                cookie.Expires= DateTime.Now.AddMinutes(30);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Popular", "Food");

            }
            return Content("Error :(");
            
        }
    }
}