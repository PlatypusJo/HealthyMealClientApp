using HealthyMeal.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace HealthyMeal.Models
{
    public class RecipeModel : BaseNutritionalValueModel
    {
        #region Свойства

        public string Id { get; set; } = null!;

        public string UserId { get; set; }

        public string FoodId { get; set; }

        public string MealTypeId { get; set; }

        public string MealTypeName { get; set; } = null!;

        public string Name { get; set; }

        public string Description { get; set; }

        #nullable enable
        public byte[]? Image { get; set; }

        public TimeSpan CookingTime { get; set; }

        public string CookingTimeString
        {
            get => CookingTime.TotalMinutes <= 59 ? 
                CookingTime.Minutes.ToString("%m") + " мин " : 
                CookingTime.Hours.ToString("%h") + " ч " + CookingTime.Minutes.ToString("%m") + " мин ";
        }

        #endregion
    }
}
