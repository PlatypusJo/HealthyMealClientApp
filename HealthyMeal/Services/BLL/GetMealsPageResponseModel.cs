using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.BLL
{
    public class GetMealsPageResponseModel
    {
        public int Count { get; set; }
        public List<MealModel> Meals { get; set; }
    }
}
