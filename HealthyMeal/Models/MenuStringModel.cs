using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class MenuStringModel : INutritionalValue
    {
        public string Id { get; set; }

        public string MealTypeId { get; set; }

        public string RecipeId { get; set; }

        public string MenuId { get; set; }

        public string RecipeName { get; set; }

        #nullable enable
        public byte[]? RecipePhoto { get; set; }

        public double Kcal { get; set; }

        public double Proteins { get; set; }

        public double Fats { get; set; }

        public double Carbohydtrates { get; set; }

        public TimeSpan CookingTime { get; set; }
    }
}
