using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class MealModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double KcalAmount { get; set; }
        public double FoodAmount { get; set; }
    }
}
