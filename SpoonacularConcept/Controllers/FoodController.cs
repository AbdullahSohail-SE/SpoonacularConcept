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

            if(Session["userLogInStatus"]!= null)
            {
                return View(Session["userLogInStatus"]);
            }
            return View();
        }

      /*  public ActionResult MarkFavourite(int recipeId)
        {

        }
      */
       [HttpPost]
        public ActionResult AddToCart(AddToLikeCart recipe)
        {

     

            var userInfo = Session["userLogInStatus"] as LoginVIewModel;
            var userId = userInfo.UserId;

            var manager = new DBManager("SpoonacularDB");
            var cartId=manager.MarkFavourite(new { userId, recipe });

            return Content(cartId.ToString());
        }
        [HttpDelete]
        [Route("food/remove/{recipeId}")]
        public ActionResult RemoveFromCart(int recipeId)
        {
            var manager = new DBManager("SpoonacularDB");

            var userInfo = Session["userLogInStatus"] as LoginVIewModel;
            var userId = userInfo.UserId;

            manager.UnmarkFavourite(recipeId, userId);

            return Content("Deleted");

        }
        public ActionResult GetLikeCount()
        {
            var manager = new DBManager("SpoonacularDB");
            var userInfo = Session["userLogInStatus"] as LoginVIewModel;
            var userId = userInfo.UserId;

            var likesCount=manager.GetLikeCount(userId);
            return Content(likesCount.ToString());
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