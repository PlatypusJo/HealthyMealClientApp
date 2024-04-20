using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class MenuModel : INutritionalValue
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public double Kcal { get; set; }

        public double Proteins { get; set; }

        public double Fats { get; set; }

        public double Carbohydtrates { get; set; }
    }
}
