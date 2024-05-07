using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.BLL
{
    public class GetRecipePageResponseModel
    {
        public int Count { get; set; }
        public List<RecipeModel> Recipes { get; set; }
    }
}
