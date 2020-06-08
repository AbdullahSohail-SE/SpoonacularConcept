using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpoonacularConcept.Models.ViewModels
{
    public class AddToLikeCart
    {
        public string Image { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public Int16 Servings { get; set; }
        public byte Score { get; set; }
        public float Price { get; set; }
        public string Time { get; set; }
        public int recipeId { get; set; }
    }
}