using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthyMeal.Models
{
    public class FoodModel : INutritionalValue
    {
        #region Свойства

        public string Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #nullable enable
        public byte[]? Image { get; set; }

        public string DefaultUnitsName { get; set; } = null!;

        public double DefaultUnitsAmount { get; set; }

        public double Kcal { get; set; }

        public double Proteins { get; set; }

        public double Fats { get; set; }

        public double Carbohydtrates { get; set; }

        #endregion
    }
}
