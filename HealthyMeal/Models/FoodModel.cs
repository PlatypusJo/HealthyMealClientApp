using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? UserId { get; set; }
        public int NutritionalValueId { get; set; }
        public int UnitsId { get; set; }
        public string UnitsName { get; set; }
        public double Kcal { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public double Amount { get; set; }
        public bool IsDefault { get; set; }
        public string Brand { get; set; }
    }
}
