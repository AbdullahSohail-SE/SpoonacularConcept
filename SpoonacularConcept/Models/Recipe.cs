using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpoonacularConcept.Models
{
    public class Recipe
    {
        public string Image { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public int Servings { get; set; }
        public int Score { get; set; }
        public float Price { get; set; }
        public string Time { get; set; }

    }
}