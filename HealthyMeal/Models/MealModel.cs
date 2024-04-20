using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class MealModel : INutritionalValue
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FoodId { get; set; }

        public string UnitsId { get; set; }

        public string MealTypeId { get; set; }

        public string UnitsName { get; set; }

        public string FoodName { get; set; }

        public DateTime Date { get; set; }

        public double AmountEaten { get; set; }

        public NutritionalValueModel NutritionalValue { private get; set; }

        public double Kcal => NutritionalValue.Kcal * AmountEaten / NutritionalValue.Amount;

        public double Proteins => NutritionalValue.Proteins * AmountEaten / NutritionalValue.Amount;

        public double Fats => NutritionalValue.Fats * AmountEaten / NutritionalValue.Amount;

        public double Carbohydtrates => NutritionalValue.Carbohydrates * AmountEaten / NutritionalValue.Amount;
    }
}
