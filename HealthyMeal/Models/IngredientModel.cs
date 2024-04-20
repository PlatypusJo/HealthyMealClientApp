using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class IngredientModel : INutritionalValue
    {
        #region Свойства

        public string Id { get; set; }

        public string RecipeId { get; set; }

        public string FoodId { get; set; }

        public string UnitsId { get; set; }

        public string Name { get; set; }

        public string UnitsName { get; set; }

        public double UnitsAmount { get; set; }

        public NutritionalValueModel NutritionalValue { private get; set; }

        public double Kcal => NutritionalValue.Kcal * UnitsAmount / NutritionalValue.Amount;

        public double Proteins => NutritionalValue.Proteins * UnitsAmount / NutritionalValue.Amount;

        public double Fats => NutritionalValue.Fats * UnitsAmount / NutritionalValue.Amount;

        public double Carbohydtrates => NutritionalValue.Carbohydrates * UnitsAmount / NutritionalValue.Amount;

        #endregion
    }
}
