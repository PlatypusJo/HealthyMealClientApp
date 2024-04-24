using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class MealModel : IMealNutritionalValue
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; }

        public string FoodId { get; set; }

        public string UnitsId { get; set; }

        public string MealTypeId { get; set; }

        public string UnitsName { get; set; }

        public string FoodName { get; set; }

        public DateTime Date { get; set; }

        public double AmountEaten { get; set; }

        public NutritionalValueModel NutritionalValue { get; set; }

        public double Kcal => NutritionalValue.Kcal * AmountEaten / NutritionalValue.UnitsAmount;

        public double Proteins => NutritionalValue.Proteins * AmountEaten / NutritionalValue.UnitsAmount;

        public double Fats => NutritionalValue.Fats * AmountEaten / NutritionalValue.UnitsAmount;

        public double Carbohydrates => NutritionalValue.Carbohydrates * AmountEaten / NutritionalValue.UnitsAmount;
    }
}
