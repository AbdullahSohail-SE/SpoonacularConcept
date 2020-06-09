using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpoonacularConcept.Models
{
    public class Ingredient
    {
        public int IngredientId;
        public string Name { get; set; }
        public string Image { get; set; }
        public string Unit { get; set; }
        public short Amount { get; set; }
    }
}