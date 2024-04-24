using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class IngredientModel : INutritionalValue
    {
        #region Свойства

        public string Id { get; set; } = null!;

        public string RecipeId { get; set; }

        public string FoodId { get; set; }

        public string UnitsId { get; set; }

        public string Name { get; set; }

        public string UnitsName { get; set; }

        public double UnitsAmount { get; set; }

        public NutritionalValueModel NutritionalValue { get; set; }

        public double Kcal => NutritionalValue.Kcal * UnitsAmount / NutritionalValue.UnitsAmount;

        public double Proteins => NutritionalValue.Proteins * UnitsAmount / NutritionalValue.UnitsAmount;

        public double Fats => NutritionalValue.Fats * UnitsAmount / NutritionalValue.UnitsAmount;

        public double Carbohydrates => NutritionalValue.Carbohydrates * UnitsAmount / NutritionalValue.UnitsAmount;

        #endregion
    }
}
