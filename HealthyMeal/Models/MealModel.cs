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

        public string InfoString => $"{Kcal} Ккал";

        public NutritionalValueModel NutritionalValue { get; set; }

        public double Kcal => NutritionalValue is not null ? NutritionalValue.Kcal * AmountEaten / NutritionalValue.UnitsAmount : 0;

        public double Proteins => NutritionalValue is not null ? NutritionalValue.Proteins * AmountEaten / NutritionalValue.UnitsAmount : 0;

        public double Fats => NutritionalValue is not null ? NutritionalValue.Fats * AmountEaten / NutritionalValue.UnitsAmount : 0;

        public double Carbohydrates => NutritionalValue is not null ? NutritionalValue.Carbohydrates * AmountEaten / NutritionalValue.UnitsAmount : 0;
    }
}
