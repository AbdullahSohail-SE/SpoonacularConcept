using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using SpoonacularConcept.Models;
using SpoonacularConcept.Models.ViewModels;

namespace SpoonacularConcept.Controllers
{
    public class FoodController : BaseController
    {
        public ActionResult Popular()
        {
            if (!CookieExists())
                return RedirectToAction("Login", "User");

            if(TempData["userLogInStatus"]!= null)
            {
                return View(TempData["userLogInStatus"]);
            }
            return View();
        }

      /*  public ActionResult MarkFavourite(int recipeId)
        {

        }
      */
       [HttpPost]
        public void AddToCart(AddToLikeCart recipe)
        {

            var time =(int) new TimeSpan(0, Convert.ToInt32(recipe.Time), 0).TotalMinutes;
            recipe.Time = time.ToString();

            var userInfo = TempData["userLogInStatus"] as LoginVIewModel;
            var userId = userInfo.UserId;

            var manager = new DBManager("SpoonacularDB");
            var cartId=manager.MarkFavourite(new { userId, recipe });

            
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