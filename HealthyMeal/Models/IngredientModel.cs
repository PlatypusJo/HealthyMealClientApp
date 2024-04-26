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

        public double Kcal => NutritionalValue is not null ? NutritionalValue.Kcal * UnitsAmount / NutritionalValue.UnitsAmount : 0;

        public double Proteins => NutritionalValue is not null ? NutritionalValue.Proteins * UnitsAmount / NutritionalValue.UnitsAmount : 0;

        public double Fats => NutritionalValue is not null ? NutritionalValue.Fats * UnitsAmount / NutritionalValue.UnitsAmount : 0;

        public double Carbohydrates => NutritionalValue is not null ? NutritionalValue.Carbohydrates * UnitsAmount / NutritionalValue.UnitsAmount : 0;

        #endregion
    }
}
