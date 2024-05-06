using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.BLL
{
    public class GetFoodPageResponseModel
    {
        public int Count { get; set; }
        public List<FoodModel> Foods { get; set; }
    }
}
