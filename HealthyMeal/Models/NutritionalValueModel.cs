using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class NutritionalValueModel : BaseNutritionalValueModel
    {
        public string Id { get; set; } = null!;

        public string FoodId { get; set; }

        public string UnitsId { get; set; }

        public double UnitsAmount { get; set; }

        public bool IsDefault { get; set; }
    }
}
