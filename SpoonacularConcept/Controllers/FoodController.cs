using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using SpoonacularConcept.Models;
using SpoonacularConcept.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace SpoonacularConcept.Controllers
{
    public class FoodController : BaseController
    {
        public ActionResult Popular()
        {
            if (!CookieExists())
                return RedirectToAction("Login", "User");

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
        [HttpPost]
        public ActionResult PurchaseIngredients(AddToLikeCart recipe)
        {
            
            
            var extendedIngredientsJarray = (JArray)JsonConvert.DeserializeObject(recipe.jsonExtendedIngredientsArray);

            var extendedIngredients = extendedIngredientsJarray.Select(x => new Ingredient()
            {
                Amount = (short)x["amount"],
                IngredientId = (int)x["id"],
                Name = (string)x["name"],
                Image = (string)x["image"],
                Unit = (string)x["unit"]
            }).ToList();


            var userInfo = Session["userLogInStatus"] as LoginVIewModel;
            var userId = userInfo.UserId;

            var manager = new DBManager("SpoonacularDB");
            manager.PurchaseIngredients(extendedIngredients, userId);
            

            return Content("asd");
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