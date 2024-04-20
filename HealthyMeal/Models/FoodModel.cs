using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthyMeal.Models
{
    public class FoodModel : BaseNutritionalValueModel
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

        #endregion
    }
}
